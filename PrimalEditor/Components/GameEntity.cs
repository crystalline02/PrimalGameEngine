using PrimalEditor.GameProject;
using PrimalEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;

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

        [DataMember]
        public Scene Owner { get; private set; }

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

        public ICommand RenameCommand { get; set; }
        public ICommand EnableCommand { get; set; }

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

            RenameCommand = new RelayCommand<string>(
                (newName) =>
                {
                    string oldName = Name;
                    Name = newName;
                    Project.UndoRedoManager.Add(new UndoRedoAction(this, nameof(Name), oldName, newName, $"Rename GameEntity '{oldName}' to '{newName}'"));
                },
                (newName) => Name != newName
                );

            EnableCommand = new RelayCommand<bool>(
                (newEnable) =>
                {
                    bool oldEnbale = IsEnable;
                    IsEnable = newEnable;
                    Project.UndoRedoManager.Add(new UndoRedoAction(this, nameof(IsEnable), oldEnbale, newEnable, $"Set GameEntity({Name}) IsEnable to {newEnable}"));
                }
                //(newEnable) => IsEnable != newEnable // Don't do this.Otherwise the IsEnabel checkbox is always disabed.
                );
        }

        public GameEntity(Scene scene, string name)
        {
            Debug.Assert(scene != null);
            Owner = scene;
            Name = name;

            OnDeserialized(new StreamingContext());
        }
    }
}
