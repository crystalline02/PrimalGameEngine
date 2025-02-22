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

        /*此处我们的容器需要存放MSComponent，但是它是一个模板类，需要显示指定模板类型T，但是列表中存放的MSComponent是具有不同模板类型的实例，
         * 为此不得不通过使MSComponent继承自一个Interface来实现*/
        private ObservableCollection<IMSComponent> _components = new ObservableCollection<IMSComponent>();
        public ReadOnlyObservableCollection<IMSComponent> Components { get; private set; }

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

        public ICommand RenameCommand { get; set; }
        public ICommand EnableCommand { get; set; }

        // Enable or disable the update for all selected entities
        protected bool _enableUpdate = true;

        protected object? GetMixedValue(string propertyName)
        {
            PropertyInfo? firstInfo = SelectedEntities.FirstOrDefault()?.GetType().GetProperty(propertyName);
            if (firstInfo != null && firstInfo.CanRead)
            {
                Type propertyType = firstInfo.PropertyType;
                var firstValue = Convert.ChangeType(firstInfo.GetValue(SelectedEntities.FirstOrDefault()), propertyType);
                foreach (GameEntity entity in SelectedEntities.Skip(1))
                {
                    PropertyInfo? info = entity.GetType().GetProperty(propertyName);
                    if (info != null && info.CanRead && firstValue != null)
                    {
                        var value = Convert.ChangeType(info.GetValue(entity), propertyType);
                        if(!firstValue.Equals(value))
                        {
                            return null;
                        }
                    }
                }
                return firstValue;
            }
            return null;
        }

        protected virtual bool UpdateMSEntityProperty()
        {
            Name = (string?)GetMixedValue(nameof(Name));
            IsEnable = (bool?)GetMixedValue(nameof(IsEnable));
            return true;
        }

        protected void Refresh()
        {
            // 只有更新属性才能使得UI界面及时更新ViewModel中的绑定值
            _enableUpdate = false;
            UpdateMSEntityProperty();
            _enableUpdate = true;
        }

        protected bool UpdateGameEntitiesProperty(string propertyName)
        {
            if (!_enableUpdate)
                return false;
            
            SelectedEntities.ForEach(entity =>
            {
                PropertyInfo? gameEntityPropertyInfo = entity?.GetType().GetProperty(propertyName);  // 如果是GameEntity的子类的属性，也能获取到
                PropertyInfo? msGameEntityPropertyInfo = this.GetType().GetProperty(propertyName);  // 如果是MSEntity的子类的属性，也应当能获取到
                if (gameEntityPropertyInfo != null && msGameEntityPropertyInfo != null && 
                    gameEntityPropertyInfo.CanWrite && msGameEntityPropertyInfo.CanRead)
                {
                    gameEntityPropertyInfo.SetValue(entity, msGameEntityPropertyInfo.GetValue(this));
                }
            });
            return true;
        }

        public MSEntity(List<GameEntity> entities)
        {
            SelectedEntities = entities;

            RenameCommand = new RelayCommand<string>(
                (newName) =>
                {
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

            // 注意是先属性被修改，然后再触发PropertyChanged事件
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
