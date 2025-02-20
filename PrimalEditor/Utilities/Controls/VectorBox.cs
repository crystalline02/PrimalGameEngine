using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrimalEditor.Utilities.Controls
{
    public enum VectorDemension
    {
        Vector2,
        Vector3,
        Vector4,
    }

    internal class VectorBox: Control
    {
        public VectorBox()
        {
            DefaultStyleKey = typeof(VectorBox);
        }
        public Orientation Orientation
        {
            get
            {
                return (Orientation)GetValue(OrientationProperty);
            }
            set 
            {
                SetValue(OrientationProperty, value); 
            }
        }
        private static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation),
            typeof(Orientation),
            typeof(VectorBox), 
            new PropertyMetadata(Orientation.Horizontal));

        public VectorDemension Demension
        {
            get
            {
                return (VectorDemension)GetValue(DemensionProperty);
            }
            set 
            { 
                SetValue(DemensionProperty, value); 
            }
        }
        private static readonly DependencyProperty DemensionProperty = DependencyProperty.Register(nameof(Demension), 
            typeof(VectorDemension),
            typeof(VectorBox), 
            new PropertyMetadata(VectorDemension.Vector3));

        public string X
        {
            get
            {
                return (string)GetValue(XProperty);
            }
            set
            {
                SetValue(XProperty, value);
            }
        }

        private static readonly DependencyProperty XProperty = DependencyProperty.Register(nameof(X), 
            typeof(string), 
            typeof(NumberBox),
            new PropertyMetadata("0"));

        public string Y
        {
            get
            {
                return (string)GetValue(YProperty);
            }
            set
            {
                SetValue(YProperty, value);
            }
        }

        private static readonly DependencyProperty YProperty = DependencyProperty.Register(nameof(Y),
            typeof(string),
            typeof(NumberBox),
            new PropertyMetadata("0"));

        public string Z
        {
            get
            {
                return (string)GetValue(ZProperty);
            }
            set
            {
                SetValue(ZProperty, value);
            }
        }

        private static readonly DependencyProperty ZProperty = DependencyProperty.Register(nameof(Z),
            typeof(string),
            typeof(NumberBox),
            new PropertyMetadata("0"));

        public string W
        {
            get
            {
                return (string)GetValue(WProperty);
            }
            set
            {
                SetValue(WProperty, value);
            }
        }

        private static readonly DependencyProperty WProperty = DependencyProperty.Register(nameof(W),
            typeof(string),
            typeof(NumberBox),
            new PropertyMetadata("0"));

        public double Mutiplier
        {
            get
            {
                return (double)GetValue(MutiplierProperty);
            }
            set
            {
                SetValue(MutiplierProperty, value);
            }
        }

        private static readonly DependencyProperty MutiplierProperty = DependencyProperty.Register(nameof(Mutiplier),
            typeof(double),
            typeof(NumberBox),
            new PropertyMetadata(1.0));
    }
}
