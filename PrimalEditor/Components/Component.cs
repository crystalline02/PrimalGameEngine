using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Components
{
    [DataContract]
    internal abstract class Component: ViewModelBase
    {
        private string _name = "Default Component";
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

        [DataMember]
        public GameEntity? Owner { get; private set; }
        public abstract IMSComponent GetMSComponentVariant(MSEntity msEntity);
        public Component(string name, GameEntity gameEntity)
        {
            _name = name;
            Owner = gameEntity;
        }
    }

    internal interface IMSComponent { }

    // T denotes which type of component, eg, transform, script and so on
    internal abstract class MSComponent<T>: ViewModelBase, IMSComponent  where T : Component
    {
        // Actual  component(T) list of the current MSComponent
        public List<T> SelectedComponents { get; set; }
        private bool _enableSourceUpdate = true;

        // 'propertyName' denotes the property of 'MSComponent'
        protected abstract bool UpdateComponentsProperty(string propertyName);
        protected abstract bool UpdateMSComponentProperty();

        // Update the property of the MSComponent using its corresponding `SelectedComponents`
        public void Refresh()
        {
            _enableSourceUpdate = false;
            UpdateMSComponentProperty();
            _enableSourceUpdate = true;
        }

        public MSComponent(MSEntity msEntity)
        {
            SelectedComponents = msEntity.GetComponentList<T>();
            PropertyChanged += (s, e) =>
            {
                if(_enableSourceUpdate)
                    UpdateComponentsProperty(e.PropertyName!);
            };
        }

        public MSComponent(List<T> selectedComponents)
        {
            SelectedComponents = selectedComponents;
            PropertyChanged += (s, e) =>
            {
                if (_enableSourceUpdate)
                    UpdateComponentsProperty(e.PropertyName!);
            };
        }
    }
}
