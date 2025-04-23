using PrimalEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Markup.Localizer;

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

        public float PosX => _position.X;
        public float PosY=> _position.Y;
        public float PosZ => _position.Z;
        public float RotationX => _rotation.X;
        public float RotationY => _rotation.Y;
        public float RotationZ => _rotation.Z;
        public float ScaleX => _scale.X;
        public float ScaleY => _scale.Y;
        public float ScaleZ => _scale.Z;

        public override IMSComponent GetMSComponentVariant(MSEntity msEntity)
        {
            return new MSTransform(msEntity);
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

    internal sealed class MSTransform : MSComponent<Transform>
    {
        static private bool _isExpanded = false;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        private float? _posX;
        public float? PosX
        {
            get => _posX;
            set
            {
                if (!_posX.IsTheSame(value))
                {
                    _posX = value;
                    OnPropertyChanged(nameof(PosX));
                }
            }
        }
        private float? _posY;
        public float? PosY
        {
            get => _posY;
            set
            {
                if (!_posY.IsTheSame(value))
                {
                    _posY = value;
                    OnPropertyChanged(nameof(PosY));
                }
            }
        }
        private float? _posZ;
        public float? PosZ
        {
            get => _posZ;
            set
            {
                if (!_posZ.IsTheSame(value))
                {
                    _posZ = value;
                    OnPropertyChanged(nameof(PosZ));
                }
            }
        }
        private float? _scaleX;
        public float? ScaleX
        {
            get => _scaleX;
            set
            {
                if (!_scaleX.IsTheSame(value))
                {
                    _scaleX = value;
                    OnPropertyChanged(nameof(ScaleX));
                }
            }
        }
        private float? _scaleY;
        public float? ScaleY
        {
            get => _scaleY;
            set
            {
                if (!_scaleY.IsTheSame(value))
                {
                    _scaleY = value;
                    OnPropertyChanged(nameof(ScaleY));
                }
            }
        }

        private float? _scaleZ;
        public float? ScaleZ
        {
            get => _scaleZ;
            set
            {
                if (!_scaleZ.IsTheSame(value))
                {
                    _scaleZ = value;
                    OnPropertyChanged(nameof(ScaleZ));
                }
            }
        }

        private float? _rotationX;
        public float? RotationX
        {
            get => _rotationX;
            set
            {
                if (!_rotationX.IsTheSame(value))
                {
                    _rotationX = value;
                    OnPropertyChanged(nameof(RotationX));
                }
            }
        }
        private float? _rotationY;
        public float? RotationY
        {
            get => _rotationY;
            set
            {
                if (!_rotationY.IsTheSame(value))
                {
                    _rotationY = value;
                    OnPropertyChanged(nameof(RotationY));
                }
            }
        }

        private float? _rotationZ;
        public float? RotationZ
        {
            get => _rotationZ;
            set
            {
                if (!_rotationZ.IsTheSame(value))
                {
                    _rotationZ = value;
                    OnPropertyChanged(nameof(RotationZ));
                }
            }
        }

        // 'propertyName' denotes the property name of 'MSTransform'
        protected override bool UpdateComponentsProperty(string propertyName)
        {
            SelectedComponents.ForEach(comp =>
            {
                switch(propertyName)
                {
                    case nameof(PosX):
                        comp.Position = new Vector3(PosX.Value, comp.PosY, comp.PosZ);
                        break;
                    case nameof(PosY):
                        Debug.WriteLine($"Set {comp} PosY to {PosY}");
                        comp.Position = new Vector3(comp.PosX, PosY.Value, comp.PosZ);
                        break;
                    case nameof(PosZ):
                        comp.Position = new Vector3(comp.PosX, comp.PosZ, PosZ.Value);
                        break;
                    case nameof(RotationX):
                        comp.Rotation = new Vector3(RotationX.Value, comp.RotationY, comp.RotationZ);
                        break;
                    case nameof(RotationY):
                        comp.Rotation = new Vector3(comp.RotationX, RotationY.Value, comp.RotationZ);
                        break;
                    case nameof(RotationZ):
                        comp.Rotation = new Vector3(comp.RotationX, comp.RotationY, RotationZ.Value);
                        break;
                    case nameof(ScaleX):
                        comp.Scale = new Vector3(ScaleX.Value, comp.ScaleY, comp.ScaleZ);
                        break;
                    case nameof(ScaleY):
                        comp.Scale = new Vector3(comp.ScaleX, ScaleY.Value, comp.ScaleZ);
                        break;
                    case nameof(ScaleZ):
                        comp.Scale = new Vector3(comp.ScaleX, comp.ScaleY, ScaleZ.Value);
                        break;
                    default:
                        break;
                }

            });
            return true;
        }

        protected override bool UpdateMSComponentProperty()
        {
            PosX = (float ?)MSEntity.GetMixedValue(nameof(Transform.PosX), SelectedComponents);
            PosY = (float?)MSEntity.GetMixedValue(nameof(Transform.PosY), SelectedComponents);
            PosZ = (float?)MSEntity.GetMixedValue(nameof(Transform.PosZ), SelectedComponents);
            RotationX = (float?)MSEntity.GetMixedValue(nameof(Transform.RotationX), SelectedComponents);
            RotationY = (float?)MSEntity.GetMixedValue(nameof(Transform.RotationY), SelectedComponents);
            RotationZ= (float?)MSEntity.GetMixedValue(nameof(Transform.RotationZ), SelectedComponents);
            ScaleX = (float?)MSEntity.GetMixedValue(nameof(Transform.ScaleX), SelectedComponents);
            ScaleY = (float?)MSEntity.GetMixedValue(nameof(Transform.ScaleY), SelectedComponents);
            ScaleZ = (float?)MSEntity.GetMixedValue(nameof(Transform.ScaleZ), SelectedComponents);

            return true;
        }

        public MSTransform(MSEntity msEntity) : base(msEntity)
        {
            Refresh();
        }
    }
}
