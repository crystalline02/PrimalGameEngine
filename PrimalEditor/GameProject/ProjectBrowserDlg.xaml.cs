using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PrimalEditor.GameProject
{
    /// <summary>
    /// Interaction logic for ProjectBrowserDlg.xaml
    /// </summary>
    public partial class ProjectBrowserDlg : Window
    {
        public ProjectBrowserDlg()
        {
            InitializeComponent();
            Loaded += OnOpenButNoRecentProject;
        }

        private void OnOpenButNoRecentProject(object sender, RoutedEventArgs e)
        {
            Loaded -= OnOpenButNoRecentProject;
            if(!OpenProject.Projects.Any())
            {
                openProjectButton.IsChecked = false;
                openProjectButton.IsEnabled = false;
                openProjectButton.Visibility = Visibility.Hidden;

                OnToggleButtonClicked(newProjectButton, new RoutedEventArgs());
            }
        }

        private void OnToggleButtonClicked(object sender, RoutedEventArgs e)
        {
            if(sender == openProjectButton)
            {
                if(newProjectButton.IsChecked == true)
                {
                    newProjectButton.IsChecked = false;
                    browserPanel.Margin = new Thickness(0, 0, 0, 0);
                }
                openProjectButton.IsChecked = true;
            }
            else  // sender == newProjectButton
            {
                if (openProjectButton.IsChecked == true)
                {
                    openProjectButton.IsChecked = false;
                    browserPanel.Margin = new Thickness(-800, 0, 0, 0);
                }
                newProjectButton.IsChecked = true;
            }
        }
    }
}
