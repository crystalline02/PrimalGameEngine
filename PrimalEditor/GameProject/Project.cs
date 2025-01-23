using PrimalEditor.Utilities;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;

namespace PrimalEditor.GameProject
{
    // Name 表示xml序列化的根名称
    [DataContract(Name = "Game")]
    class Project : ViewModelBase
    {
        // 无set表示该属性只允许在构造函数中初始化一次
        // private set表示属性只能在类内部修改，不仅仅局限于构造函数
        [DataMember]
        public string Name { get; private set; }  // 创建界面的项目名称
        [DataMember]
        public string Path { get; private set; }  // 创建界面的项目路径

        static public string Extention => ".primal";

        public string FilePath => System.IO.Path.GetFullPath(System.IO.Path.Combine($"{Path}", $"{Name}{Extention}"));  // 项目文件的完整路径

        [DataMember(Name = nameof(Scenes))]
        private ObservableCollection<Scene> _scenes = new ObservableCollection<Scene>();
        public ReadOnlyObservableCollection<Scene> Scenes  // Needs to be intialized in 'OnDeserialized'
        {
            get; private set;
        }

        public Scene? ActiveScene { get; set; }

        public static Project? Current => Application.Current?.MainWindow?.DataContext as Project;

        public static Project? Load(string path)
        {
            // Path: .primal path
            if (!string.IsNullOrEmpty(path.Trim()) && File.Exists(path))
                return Utilities.Serializier.FromFile<Project>(path);
            return null;
        }

        public void UnLoad()
        {
            UndoRedoManager.Clear();
        }

        public static void Save(Project project)
        {
            Utilities.Serializier.ToFile(project, project.FilePath);
            Logger.Log($"Successfully saved the project {project.Name}", MessageType.Info);
        }

        private void AddSceneInternal(string name = "New Scene")
        {
            _scenes.Add(new Scene(name, this));
        }

        private void RemoveSceneInternal(Scene scene)
        {
            _scenes.Remove(scene);
        }

        public static UndoRedo UndoRedoManager { get; } = new UndoRedo();

        public ICommand AddSceneCommand { get; private set; }
        public ICommand RemoveSceneCommand { get; private set; }
        public ICommand SaveProjectCommand { get; private set; }

        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set;  }

        /*该方法在Serializier.FromFile返回一个Project后自动调用，用于给Project初始化字段属性，因为Serializier.FromFile初始化的Project并不
        通过构造函数调用，而且属性可能初始化不完全（没有被DataMemberAttribute标记的属性字段），所以需要这个函数
        
        在项目中，主窗口的DataContext作为Project，都是通过反序列化获得，如果我们要初始化Project中的什么属性，需要在该函数中处理
         */
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_scenes != null)
            {
                Scenes = new ReadOnlyObservableCollection<Scene>(_scenes);
                OnPropertyChanged(nameof(Scenes));
                ActiveScene = _scenes.FirstOrDefault(x => x.IsActive);
            }

            AddSceneCommand = new RelayCommand<object>(
                (x) =>
                {
                AddSceneInternal($"New Scene {Scenes.Count}");
                    var newScene = _scenes.Last();
                    int index = _scenes.Count - 1;
                    UndoRedoAction action = new UndoRedoAction(
                        () => RemoveSceneInternal(newScene),
                        () => _scenes.Insert(index, newScene),
                        $"Add scene {newScene.Name} action"
                        );
                    UndoRedoManager.Add(action);
                }
             );

            RemoveSceneCommand = new RelayCommand<Scene>(
                (x)=>
                {
                    int index = _scenes.IndexOf(x);
                    RemoveSceneInternal(x);
                    UndoRedoAction action = new UndoRedoAction(
                        ()=>  _scenes.Insert(index, x),
                        ()=> RemoveSceneInternal(x),
                        $"Remove scene {x.Name} action"
                        );
                    UndoRedoManager.Add(action);
                }
                );

            UndoCommand = new RelayCommand<object>(
                (x) => UndoRedoManager.Undo(),
                (x) => UndoRedoManager.CanUndo()
                );

            RedoCommand = new RelayCommand<object>(
                (x) => UndoRedoManager.Redo(),
                (x) => UndoRedoManager.CanRedo()
                );

            SaveProjectCommand = new RelayCommand<Project>(
                (x) => Save(x)
                );
        }

        // 本项目不会手动添加Project，都是通过反序列化获得Project，该构造函数理论永不会执行
        public Project(string name, string path) 
        {
            Name = name;
            Path = path;
            _scenes.Add(new Scene("Default Scene", this));

            OnDeserialized(new StreamingContext());
        }
    }
}
