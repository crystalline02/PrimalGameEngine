using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PrimalEditor.Components;
using PrimalEditor.GameProject;
using PrimalEditor.Utilities;

namespace PrimalEditor.Editors
{
    /// <summary>
    /// Interaction logic for ProjectLayoutControl.xaml
    /// </summary>
    public partial class ProjectLayoutControl : UserControl
    {
        public ProjectLayoutControl()
        {
            InitializeComponent();
        }

        private void OnSceneAddEntityClicked(object sender, RoutedEventArgs e)
        {
            Scene? s = ((sender as Button).DataContext as Scene);
            s?.AddEntityCommand.Execute(new GameEntity(s, "Default Entity"));
        }

        private void OnGameEntitySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox? entityListBox = (sender as ListBox);
            List<GameEntity>? currentSelections = entityListBox?.SelectedItems?.Cast<GameEntity>().ToList();
            List<GameEntity>? prevSelection = (currentSelections?.
                Except(e.AddedItems?.Cast<GameEntity>()).
                Concat(e.RemovedItems?.Cast<GameEntity>()).ToList());

            if(currentSelections != null && currentSelections.Any())
            {
                MSGameEntity msGamEntity = new MSGameEntity(currentSelections);
                GameEntityControl.Instance.DataContext = msGamEntity; 
            }
            else
            {
                GameEntityControl.Instance.DataContext = null;
            }
            
            // selection changed undo and redo
            Project.UndoRedoManager.Add(new UndoRedoAction(
                () =>
                {
                    entityListBox?.UnselectAll();
                    prevSelection?.ForEach(x => (entityListBox?.ItemContainerGenerator.ContainerFromItem(x) as ListBoxItem).IsSelected = true);
                },
                () =>
                {
                    entityListBox?.UnselectAll();
                    currentSelections?.ForEach(x => (entityListBox?.ItemContainerGenerator.ContainerFromItem(x) as ListBoxItem).IsSelected = true);
                },
                "Game entities selection changed")
                );
        }
    }
}
