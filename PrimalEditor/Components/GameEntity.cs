using PrimalEditor.GameProject;
using PrimalEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using System.Collections.Generic;
using System.Reflection;
using System;
using PrimalEditor.DLLWrapper;
using System.Numerics;

namespace PrimalEditor.Components
{
    [DataContract]
    [KnownType(typeof(Transform))]
    internal class GameEntity: ViewModelBase
    {
        private string _name = "Defalut Entity";
        [DataMember]
        public string Name
        {
            get => _name;
            set
            {
                if(value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        [DataMember(Name = nameof(Components))]
        private ObservableCollection<Component> _components = new ObservableCollection<Component>();
        public ReadOnlyObservableCollection<Component> Components { get; private set; }

        public T? GetComponent<T>() where T: Component => _components.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
        public Component? GetComponent(Type compType) => _components.FirstOrDefault(x => x.GetType() == compType);


        [DataMember]
        public Scene Owner { get; private set; }

        private int _entityId = Id.INVALID_ID;  // Entity id represented in engine.
        public int EntityId
        {
            get => _entityId;
            set
            {
                if(value != _entityId)
                {
                    _entityId = value;
                    OnPropertyChanged(nameof(EntityId));
                }
            }
        }

        // 用于添加和删除引擎中的GameEntity
        private bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value != _isActive)
                {
                    _isActive = value;
                    if (_isActive)
                    {
                        // Add a entity to engine
                        EntityId = EngineAPI.CreateGameEntity(this);
                    }
                    else
                    {
                        // remove current entity from engine
                        Debug.Assert(EntityId != Id.INVALID_ID);
                        EngineAPI.RemoveGameEntity(this);
                        EntityId = Id.INVALID_ID;
                    }
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        private bool _isEnable;
        [DataMember]
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if(_isEnable != value)
                {
                    _isEnable = value;
                    OnPropertyChanged(nameof(IsEnable));
                }
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if(_components != null)  // 如果_components成功被反序列化，有值
            {
                if(!_components.Any())  // 保证GameEntity至少有一个Transform组件，需要修正
                {
                    _components.Add(new Transform(this));
                }

                Components = new ReadOnlyObservableCollection<Component>(_components);
                OnPropertyChanged(nameof(Components));
            }
        }

        public GameEntity(Scene scene, string name)
        {
            Debug.Assert(scene != null);
            Owner = scene;
            Name = name;

            OnDeserialized(new StreamingContext());
        }
    }

    internal abstract class MSEntity: ViewModelBase
    {
        private string? _name = "Defalut Entity";
        public string? Name
        {
            get => _name;
            set
            {
                // If the property of MSEntity is changed.The value will propagates to all the selected game entities because we registered a function to event PropertyChanged.
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /*此处我们的容器需要存放MSComponent，但是它是一个模板类，需要显示指定模板类型T，因此列表中存放的MSComponent需要是具有不同模板类型的实例，
         * 这在语法上是不允许的，为此不得不通过使MSComponent继承自一个Interface来实现*/
        private ObservableCollection<IMSComponent> _msComponents = new ObservableCollection<IMSComponent>();
        public ReadOnlyObservableCollection<IMSComponent> MSComponents { get; private set; }

        private bool? _isEnable;
        public bool? IsEnable
        {
            get => _isEnable;
            set
            {
                if (_isEnable != value)
                {
                    _isEnable = value;
                    OnPropertyChanged(nameof(IsEnable));
                }
            }
        }

        private List<GameEntity> SelectedEntities { get; set; }
        public List<T> GetComponentList<T>() where T: Component
        {
            if (SelectedEntities != null)
            {
                List<T> compList = SelectedEntities.Select(entity => entity.GetComponent<T>()).ToList();
                compList.RemoveAll(comp => comp == null);
                return compList;
            }
            return new List<T>();
        }

        public ICommand RenameCommand { get; set; }
        public ICommand EnableCommand { get; set; }

        // Enable or disable the update for all selected entities
        protected bool _enableUpdate = true;

        // 'propertyName' donotes the property of 'GameEntity'
        static public object? GetMixedValue<T>(string propertyName, List<T> instances)
        {
            if(!instances.Any())
                return null;
            
            // 第一个实例没有指定的属性名称，则对于的MixedValue就是null
            PropertyInfo? firstInfo = instances.FirstOrDefault()?.GetType().GetProperty(propertyName);
            if(firstInfo == null || !firstInfo.CanRead)
                return null;
            Type propertyType = firstInfo.PropertyType;
            object firstValue = Convert.ChangeType(firstInfo.GetValue(instances.FirstOrDefault()), propertyType)!;

            if (instances.Any(instance =>
            {
                PropertyInfo? info = instance?.GetType().GetProperty(propertyName);

                // 某实例没有指定名称的属性
                if(info == null || !info.CanRead)
                    return true;

                // 某实例有指定名称的属性但是值于第一个不匹配
                if (propertyType == typeof(string))
                {
                    string firstValueStr = (string)firstValue;
                    string value = (string)Convert.ChangeType(info.GetValue(instance), typeof(string))!;
                    return value != firstValueStr;
                }
                else if (propertyType == typeof(bool))
                {
                    bool firstValueBool = (bool)firstValue;
                    bool value = (bool)Convert.ChangeType(info.GetValue(instance), typeof(bool))!;
                    return value != firstValueBool;
                }
                else if(propertyType == typeof(int))
                {
                    int firstValueInt = (int)firstValue;
                    int value = (int)Convert.ChangeType(info.GetValue(instance), typeof(int))!;
                    return value != firstValueInt;
                }
                else if (propertyType == typeof(float))
                {
                    float firstValueFloat = (float)firstValue;
                    float value = (float)Convert.ChangeType(info.GetValue(instance), typeof(float))!;
                    return !firstValueFloat.IsTheSame(value);
                }
                else if (propertyType == typeof(double))
                {
                    double firstValueDouble = (double)firstValue;
                    double value = (double)Convert.ChangeType(info.GetValue(instance), typeof(double))!;
                    return !firstValueDouble.IsTheSame(value);
                }
                else if (propertyType == typeof(Vector3))
                {
                    Vector3 firstValueVector = (Vector3)firstValue;
                    Vector3 value = (Vector3)Convert.ChangeType(info.GetValue(instance), typeof(Vector3))!;
                    return !firstValueVector.IsTheSame(value);
                }
                else
                    return true;

            }))
                return null;

            return firstValue;


            /*PropertyInfo? firstInfo = elements.FirstOrDefault()?.GetType().GetProperty(propertyName);
            if (firstInfo != null && firstInfo.CanRead)
            {
                Type propertyType = firstInfo.PropertyType;
                var firstValue = Convert.ChangeType(firstInfo.GetValue(elements.FirstOrDefault()), propertyType);
                If there is any entity whose corresbonding property is not the same as the first one.
                if (elements.Any(entity =>
                    {
                        PropertyInfo? info = entity?.GetType().GetProperty(propertyName);
                        if (info != null && info.CanRead && firstValue != null)
                        {
                            var value = Convert.ChangeType(info.GetValue(entity), propertyType);
                            if (!MathUtils.IsTheSame(firstValue, value))
                                return false;
                        }
                        return false;
                    }))
                {
                    return null;
                }
                return firstValue;
            }
            return null;*/
        }

        protected virtual bool UpdateMSEntityProperty()
        {
            Name = (string?)GetMixedValue(nameof(GameEntity.Name), SelectedEntities);
            IsEnable = (bool?)GetMixedValue(nameof(GameEntity.IsEnable), SelectedEntities);
            return true;
        }

        protected void Refresh()
        {
            // 只有更新属性才能使得UI界面及时更新ViewModel中的绑定值
            _enableUpdate = false;
            UpdateMSEntityProperty();
            _enableUpdate = true;
        }

        // propertyName denotes the property of 'MSGameEntity'
        protected virtual bool UpdateGameEntitiesProperty(string propertyName)
        {
            if (!_enableUpdate)
                return false;
            
            SelectedEntities.ForEach(entity =>
            {
                switch(propertyName)
                {
                    case nameof(Name):
                        if(Name != null)
                            entity.Name = Name;
                        return;
                    case nameof(IsEnable):
                        if(IsEnable != null)
                            entity.IsEnable = (bool)IsEnable;
                        return;
                    default:
                        return;
                }
            });
            return true;
        }

        private void MakeMSComponentsList()
        {
            _msComponents.Clear();
            MSComponents = new ReadOnlyObservableCollection<IMSComponent>(_msComponents);

            GameEntity? firstEntity = SelectedEntities.FirstOrDefault();
            if(firstEntity != null)
            {
                // 'comp' could be 'Transform','Script','RigidBody'.......We need its ms variants like 'MSTransform', 'MScrpt', 'MSRigBody'.This falls into polomophic.
                foreach (Component comp in firstEntity.Components)
                {
                    Type compType = comp.GetType();
                    if (!SelectedEntities.Skip(1).Any(entity => entity.GetComponent(compType) == null))
                    {
                        _msComponents.Add(comp.GetMSComponentVariant(this));
                    }
                 }
            }
            
        }

        public MSEntity(List<GameEntity> entities)
        {
            SelectedEntities = entities;
            MakeMSComponentsList();

            RenameCommand = new RelayCommand<string>(
                (newName) =>
                {
                    // Ban empty name game entity
                    if(string.IsNullOrEmpty(newName))
                        return;

                    List<(GameEntity, string)> entitiesOldname = SelectedEntities.Select((entity) => (entity, entity.Name)).ToList();
                    Name = newName;  // 更新属性并同步到所有选中的GameEntity
                    Project.UndoRedoManager.Add(
                        new UndoRedoAction(
                            () =>
                            {
                                entitiesOldname.ForEach((item) =>
                                {
                                    item.Item1.Name = item.Item2;
                                });
                                Refresh();
                            },
                            () =>
                            {
                                Name = newName;
                                Refresh();
                            },
                            $"Rename seleted game entities to {newName}"));
                },
                (newName) =>
                {
                    return Name != newName;
                });

            EnableCommand = new RelayCommand<bool>(
                (newEnable) =>
                {
                    List<(GameEntity, bool)> entitiesOldEnable = SelectedEntities.Select((entity) => (entity, entity.IsEnable)).ToList();
                    IsEnable = newEnable;  // 更新属性并同步到所有选中的GameEntity
                    Project.UndoRedoManager.Add(new UndoRedoAction(() =>
                    {
                        entitiesOldEnable.ForEach((item) =>
                        {
                            item.Item1.IsEnable = item.Item2;
                        });
                        Refresh();
                    },
                    () =>
                    {
                        IsEnable = newEnable;
                        Refresh();
                    },
                    newEnable ? "Enable selected game entities" : "Disable selected game entities"));
                });

            // 注意是先MSEntity的属性被修改，然后再触发PropertyChanged事件
            PropertyChanged += (s, e) =>
            {
                UpdateGameEntitiesProperty(e.PropertyName);
            };
        }
    }

    internal class MSGameEntity: MSEntity
    {
        public MSGameEntity(List<GameEntity> entities): base(entities)
        {
            Refresh();
        }
    }
}
