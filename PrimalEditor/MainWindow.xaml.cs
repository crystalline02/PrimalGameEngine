using PrimalEditor.GameProject;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnMainWindowLoaded;
            Closed += OnMainWindowClosed;
        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Once events in 'Loaded' is called, unregister this function so that it won't be called next time initializing main window.
            Loaded -= OnMainWindowLoaded;
            ShowProjectBrowserDlg();
        }

        private void OnMainWindowClosed(object? sender, EventArgs e)
        {
            Closed -= OnMainWindowClosed;
            Project.Current?.UnLoad();
        }

        private void ShowProjectBrowserDlg()
        {
            var dlg = new ProjectBrowserDlg();
            if(dlg.ShowDialog() == true && dlg.DataContext is Project project)
            {
                Project.Current?.UnLoad();  // (DataContext as Project)?.UnLoad(); is the same
                DataContext = project;  // 这个DataContext会被所有嵌套在MainWindow的UserControl的DataContext所继承，这个机制很有价值
                Debug.WriteLine($"Successfully open project {project.Name} in main window.");
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
