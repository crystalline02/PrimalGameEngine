using PrimalEditor.Components;
using PrimalEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrimalEditor.GameProject
{
    /* 
    这里需要IsReference = true（在ViewModelBase中设置了，保证所有继承自该类的类都有这样的序列化机制），
    这样做是为了防止循环引用导致的序列化无限递归的问题，Scene中有Project，Project中也会有这个Scene，所以做此设置
    */
    [DataContract]
    internal class Scene: ViewModelBase
    {
        private bool _isActive = false;
        [DataMember]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if(_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }   
            }
        }

        // scene的命名
        private string _name = "Default Scene";
        [DataMember]
        public string Name {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }

        }

        // scene所属的project
        [DataMember]
        public Project Project { get; private set; }

        [DataMember(Name = nameof(GameEntities))]
        private ObservableCollection<GameEntity> _gameEntities = new ObservableCollection<GameEntity>();
        public ReadOnlyObservableCollection<GameEntity> GameEntities { get; private set; }

        public ICommand AddEntityCommand { get; private set; }
        public ICommand RemoveEntityCommand { get; private set; }

        private void AddEntityInternal(GameEntity entity)
        {
            _gameEntities.Add(entity);
        }

        private void RemoveEntityInternal(GameEntity entity)
        {
            _gameEntities.Remove(entity);
        }

        // 打开或者新建项目不走构造函数，走这个OnDeserialized
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // _gameEntities通过反序列化已经获得，是空的一个列表
            if(_gameEntities != null)
            {
                GameEntities = new ReadOnlyObservableCollection<GameEntity>(_gameEntities);
                OnPropertyChanged(nameof(GameEntities));
            }

            AddEntityCommand = new RelayCommand<GameEntity>(
                (x) =>
                {
                    AddEntityInternal(x);
                    int entityIndex = _gameEntities.Count - 1;
                    UndoRedoAction action = new UndoRedoAction(
                        () => RemoveEntityInternal(x),
                        () => _gameEntities.Insert(entityIndex, x),
                        $"Add Entity({x.Name}) to Scene({Name})"
                        );
                    Project.UndoRedoManager.Add(action);
                }
                );

            RemoveEntityCommand = new RelayCommand<GameEntity>(
                (x) =>
                {
                    int entityIndex = _gameEntities.IndexOf(x);
                    RemoveEntityInternal(x);
                    UndoRedoAction action = new UndoRedoAction(
                        () => _gameEntities.Insert(entityIndex, x),
                        () => RemoveEntityInternal(x),
                        $"Remove Entity({x.Name}) form Scene({Name}) "
                        );
                    Project.UndoRedoManager.Add(action);
                }
                );
        }

        // 手动在交互界面添加Scene的时候会调用构造函数
        public Scene(string name, Project project)
        {
            Debug.Assert(project != null);
            Name = name;
            Project = project;

            OnDeserialized(new StreamingContext());
        }

    }
}
