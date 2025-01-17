using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrimalEditor.GameProject
{
    /// <summary>
    /// Interaction logic for OpenProjectControl.xaml
    /// </summary>
    public partial class OpenProjectControl : UserControl
    {
        public OpenProjectControl()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                /*设置默认被选中的ProjectData的Focus，使其被键盘聚焦*/
                ListBoxItem? defaultProjectItem = projectsDataListBox.ItemContainerGenerator.ContainerFromIndex(projectsDataListBox.SelectedIndex) as ListBoxItem;
                defaultProjectItem?.Focus();
            };
        }

        private void OnProjectInListBoxDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            OpenSelectedProject();
        }

        private void OnOpenButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenSelectedProject();
        }

        private void OpenSelectedProject()
        {
            Window w = Window.GetWindow(this);  // ProjectBrwserDlg
            bool dialogResult = false;
            if (projectsDataListBox?.SelectedItem is ProjectData pd)
            {
                Project? opened = OpenProject.Open(pd);
                if (opened != null)  // open succeed
                {
                    w.DataContext = opened;
                    dialogResult = true;
                }
            }
            w.DialogResult = dialogResult;
            w.Close();
        }
    }
}
