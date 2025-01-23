using PrimalEditor.GameProject;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrimalEditor.Editors
{
    /// <summary>
    /// Interaction logic for WorldEditorControl.xaml
    /// </summary>
    public partial class WorldEditorControl : UserControl
    {
        public static WorldEditorControl? Instance;

        public WorldEditorControl()
        {
            InitializeComponent();
            Instance = this;

            Loaded += OnWorldEditorInit;
        }
        
        private void OnWorldEditorInit(object sender, RoutedEventArgs e)
        {
            Loaded -= OnWorldEditorInit;

            // Set focus of WorldEditorControl when init
            Focus();
            
            //Project.UndoRedoManager.RegisterUndoListChangeEvent((s, e) =>
            //    {
            //        Focus();
            //    });
            // ((INotifyCollectionChanged)Project.UndoRedoManager.UndoList).CollectionChanged += (s, e) => Focus();
        }
    }
}
