using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace PrimalEditor.Editors
{
    /// <summary>
    /// Interaction logic for ComponentControl.xaml
    /// </summary>
    [ContentProperty(name: nameof(ComponentControl.ComponentContent))]
    public partial class ComponentControl : UserControl
    {
        public bool SetExpanded
        {
            get { return (bool)GetValue(SetExpandedProperty); }
            set { SetValue(SetExpandedProperty, value); }
        }
        public static DependencyProperty SetExpandedProperty = DependencyProperty.Register(nameof(SetExpanded),
            typeof(bool),
            typeof(ComponentControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string ComponentName
        {
            get { return (string)GetValue(ComponentNameProperty); }
            set { SetValue(ComponentNameProperty, value); }
        }
        public static DependencyProperty ComponentNameProperty = DependencyProperty.Register(nameof(ComponentName),
            typeof(string),
            typeof(ComponentControl), 
            new PropertyMetadata("Unknow component"));

        public FrameworkElement ComponentContent
        {
            get { return (FrameworkElement)GetValue(ComponentContentProperty); }
            set { SetValue(ComponentContentProperty, value); }
        }
        public static DependencyProperty ComponentContentProperty = DependencyProperty.Register(nameof(ComponentContent),
            typeof(FrameworkElement),
            typeof(ComponentControl),
            new PropertyMetadata(default));

        public ComponentControl()
        {
            InitializeComponent();
        }
    }
}
