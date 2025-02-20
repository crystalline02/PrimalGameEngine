using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;

namespace PrimalEditor.Components
{
    [DataContract]
    internal class Transform: Component
    {
        private Vector3 _position;
        [DataMember]
        public Vector3 Position
        {
            get => _position;
            set
            {
                if(value != _position)
                {
                    _position = value;
                    OnPropertyChanged(nameof(Position));
                }
            }
        }

        private Vector3 _rotation;
        [DataMember]
        public Vector3 Rotation
        {
            get => _rotation;
            set
            {
                if (value != _rotation)
                {
                    _rotation = value;
                    OnPropertyChanged(nameof(Rotation));
                }
            }
        }

        private Vector3 _scale;
        [DataMember]
        public Vector3 Scale
        {
            get => _scale;
            set
            {
                if (value != _scale)
                {
                    _scale = value;
                    OnPropertyChanged(nameof(Scale));
                }
            }
        }

        public Transform(GameEntity gameEntity) : base("Transform", gameEntity)
        {
            _position = new Vector3(0);
            _scale = new Vector3(1);
            _rotation = new Vector3(0);
        }

        public Transform(GameEntity gameEntity, Vector3 position, Vector3 rotation, Vector3 scale): base("Transform", gameEntity)
        {
            _position = position;
            _scale = scale;
            _rotation = rotation;
        }
    }
}
