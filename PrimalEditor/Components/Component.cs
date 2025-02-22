using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        private List<Component> Components { get; set; }
        public MSComponent(MSEntity msEntity)
        {

        }
    }
}
