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
using System.Windows.Media.Animation;
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

            Loaded += (s, e) =>
            {
                OpenProjectView.IsEnabled = true;
                NewProjectView.IsEnabled = false;
            };

            Loaded += OnOpenButNoRecentProject;
        }

        private void OnOpenButNoRecentProject(object sender, RoutedEventArgs e)
        {
            Loaded -= OnOpenButNoRecentProject;
            if(!OpenProject.Projects.Any())
            {
                OpenProjectButton.IsChecked = false;
                OpenProjectButton.IsEnabled = false;
                OpenProjectButton.Visibility = Visibility.Hidden;
                OnToggleButtonClicked(NewProjectButton, new RoutedEventArgs());
            }
        }

        private CubicEase EaseFunction { get; set; } = new CubicEase() { EasingMode=EasingMode.EaseInOut };

        private void AnimateOpenProject2CreateProject()
        {
            DoubleAnimation highlightAnimation = new DoubleAnimation(150, 420, new Duration(TimeSpan.FromSeconds(0.15f)));
            highlightAnimation.EasingFunction = EaseFunction;
            highlightAnimation.Completed += (s, e) =>
            {
                ThicknessAnimation viewAnimation = new ThicknessAnimation(new Thickness(0), new Thickness(-1600, 0, 0, 0), new Duration(TimeSpan.FromSeconds(0.15)));
                viewAnimation.EasingFunction = EaseFunction;
                browserPanel.BeginAnimation(MarginProperty, viewAnimation);
            };
            SpotLightRect.BeginAnimation(Canvas.LeftProperty, highlightAnimation);
        }

        private void AnimateCreateProject2OpenProject()
        {
            DoubleAnimation highlightAnimation = new DoubleAnimation(420, 150, new Duration(TimeSpan.FromSeconds(0.15f)));
            highlightAnimation.EasingFunction = EaseFunction;
            highlightAnimation.Completed += (s, e) =>
            {
                ThicknessAnimation viewAnimation = new ThicknessAnimation(new Thickness(-1600, 0, 0, 0), new Thickness(0), new Duration(TimeSpan.FromSeconds(0.15f)));
                viewAnimation.EasingFunction = EaseFunction;
                browserPanel.BeginAnimation(MarginProperty, viewAnimation);
            };
            SpotLightRect.BeginAnimation(Canvas.LeftProperty, highlightAnimation);
        }

        private void OnToggleButtonClicked(object sender, RoutedEventArgs e)
        {
            if(sender == OpenProjectButton)
            {
                if(NewProjectButton.IsChecked == true)
                {
                    NewProjectButton.IsChecked = false;
                    AnimateCreateProject2OpenProject();
                    OpenProjectView.IsEnabled = true;
                    NewProjectView.IsEnabled = false;
                }
                OpenProjectButton.IsChecked = true;
            }
            else  // sender == NewProjectButton
            {
                if (OpenProjectButton.IsChecked == true)
                {
                    OpenProjectButton.IsChecked = false;
                    AnimateOpenProject2CreateProject();
                    OpenProjectView.IsEnabled = false;
                    NewProjectView.IsEnabled = true;
                }
                NewProjectButton.IsChecked = true;
            }
        }
    }
}
