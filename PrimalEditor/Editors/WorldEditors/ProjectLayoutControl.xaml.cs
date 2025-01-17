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
            List<GameEntity>? currentSelection = entityListBox?.SelectedItems?.Cast<GameEntity>().ToList();
            GameEntity? firstSelect = currentSelection?.FirstOrDefault();
            GameEntityControl.Instance.DataContext = firstSelect;
            List<GameEntity>? prevSelection = (currentSelection?.
                Except(e.AddedItems?.Cast<GameEntity>()).
                Concat(e.RemovedItems?.Cast<GameEntity>()).ToList());
            
            GameEntityControl.Instance.DataContext = firstSelect;
            Project.UndoRedoManager.Add(new UndoRedoAction(
                () =>
                {
                    entityListBox?.UnselectAll();
                    prevSelection?.ForEach(x => (entityListBox?.ItemContainerGenerator.ContainerFromItem(x) as ListBoxItem).IsSelected = true);
                },
                () =>
                {
                    entityListBox?.UnselectAll();
                    currentSelection?.ForEach(x => (entityListBox?.ItemContainerGenerator.ContainerFromItem(x) as ListBoxItem).IsSelected = true);
                },
                "Game entities selection changed")
                );
        }
    }
}
