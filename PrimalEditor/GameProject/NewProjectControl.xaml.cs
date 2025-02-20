using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PrimalEditor.GameProject
{
    /// <summary>
    /// Interaction logic for NewProjectControl.xaml
    /// </summary>
    public partial class NewProjectControl : UserControl
    {
        public NewProjectControl()
        {
            InitializeComponent();
        }
        private void OnCreateNewProjectBtnClicked(object sender, RoutedEventArgs e)
        {
            NewProject? np = DataContext as NewProject;
            if (np == null)
                return;
            bool dlgResult = false;

            string? path = np?.CreateNewProject(templateListBox.SelectedItem as ProjectTemplate);
            Window w = Window.GetWindow(this);  // 这个window是ProjectBrowserDlg
            if(!string.IsNullOrEmpty(path))
            {
                dlgResult = true;
                Project? project = OpenProject.Open(new ProjectData() { ProjectName = np?.ProjectName, ProjectPath = np?.GetFullProjectPath() });
                w.DataContext = project;
            }
            
            w.DialogResult = dlgResult;   // 设置模态窗口（这里是ProjectBrowserDlg）的返回结果（ShowDialog()方法）
            w.Close();
        }
    }

}
