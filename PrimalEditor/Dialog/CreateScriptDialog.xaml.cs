using PrimalEditor.GameDev;
using System;
using System.IO;
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

namespace PrimalEditor.Dialog
{
    /// <summary>
    /// Interaction logic for CreateScriptDialog.xaml
    /// </summary>
    public partial class CreateScriptDialog : System.Windows.Window
    {
        private readonly string headerContent = "#pragma once\r\n\r\nnamespace {0}\r\n{{\r\n\tclass {1} : public primal::script::entity_script\r\n\t{{\r\n\tpublic:\r\n\t\tvirtual ~{1}() = default;\r\n\t\tvoid begin_play() override;\r\n\t\tvoid update(float delta_time) override;\r\n\t\t{1}(primal::entity::game_entity e) : entity_script(e)\r\n\t\t{{\r\n\r\n\t\t}}\r\n\t}};\r\n\r\n}}";
        private readonly string cppContent = "#include \"{0}.h\"\r\n\r\n\r\nnamespace {1}\r\n{{\r\n\tREGISTER_SCRIPT({0})\r\n\r\n\tvoid {0}::begin_play()\r\n\t{{\r\n\r\n\t}}\r\n\tvoid {0}::update(float delta_time)\r\n\t{{\r\n\r\n\t}}\r\n}}";

        public CreateScriptDialog()
        {
            InitializeComponent();
            Loaded += OnOpenCreateScriptDialog;
        }

        private void OnOpenCreateScriptDialog(object sender, RoutedEventArgs e)
        {
            Loaded -= OnOpenCreateScriptDialog;
            Validate();
        }

        bool Validate()
        {
            if (NameTB == null | PathTB == null || MessageText == null)
                return false;

            string scriptName = NameTB.Text;
            string scriptRelativePath = PathTB.Text;
            if (string.IsNullOrEmpty(scriptName) ||
                scriptName.Contains(' ') ||
                scriptName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
            {
                MessageText.Text = "Invalid script file name";
                MessageText.Foreground = FindResource("Editor.RedColorBrush") as Brush;
                ConfirmBtn.IsEnabled = false;
                return false;
            }

            if(string.IsNullOrEmpty(scriptRelativePath) || 
                scriptRelativePath.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)
            {
                MessageText.Text = "Invalid script path";
                MessageText.Foreground = FindResource("Editor.RedColorBrush") as Brush;
                ConfirmBtn.IsEnabled = false;
                return false;
            }

            string scriptPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.Combine(GameProject.Project.Current.Path, "GameCode"), scriptRelativePath));
            if(System.IO.File.Exists(System.IO.Path.Combine(scriptPath, $"{scriptName}.h")) ||
                System.IO.File.Exists(System.IO.Path.Combine(scriptPath, $"{scriptName}.cpp")))
            {
                MessageText.Text = "Script file already exits!";
                MessageText.Foreground = FindResource("Editor.RedColorBrush") as Brush;
                ConfirmBtn.IsEnabled = false;
                return false;
            }

            MessageText.Text = $"{scriptName}.h and {scriptName}.cpp will be added";
            MessageText.Foreground = FindResource("Editor.FontColorBrush") as Brush;
            ConfirmBtn.IsEnabled = true;
            return true;
        }

        private async void OnCreateScriptBtnClicked(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            string scriptPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.Combine(GameProject.Project.Current.Path, "GameCode"), $"{PathTB.Text}"));
            string scriptName = NameTB.Text;
            string solutionPath = GameProject.Project.Current.SolutionPath;

            string headerPath = System.IO.Path.GetFullPath((System.IO.Path.Combine(scriptPath, $"{scriptName}.h")));
            string cppPath = System.IO.Path.GetFullPath((System.IO.Path.Combine(scriptPath, $"{scriptName}.cpp")));

            string headerContentFormated = string.Format(headerContent, GameProject.Project.Current.Name.Replace(" ", "_"), scriptName);
            string cppContentFormated = string.Format(cppContent, scriptName, GameProject.Project.Current.Name.Replace(" ", "_"));
            
            BusyAnimationUIGrid.Visibility = Visibility.Visible;
            InputUIGrid.IsEnabled = false;
            DoubleAnimation busyOpacityShowAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(1500)));

            BusyAnimationUIGrid.BeginAnimation(Grid.OpacityProperty, busyOpacityShowAnimation);
            await Task.Run(() =>
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(headerPath));
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(cppPath));
                StreamWriter writerHeader = new StreamWriter(headerPath);
                StreamWriter writerCpp = new StreamWriter(cppPath);
                writerHeader.Write(headerContentFormated);
                writerCpp.Write(cppContentFormated);
                writerCpp.Close();
                writerHeader.Close();

                for(int i = 0; i < 3; i++)
                {
                    if (!VisualStudio.AddScriptToSolution(cppPath, headerPath, solutionPath))
                        System.Threading.Thread.Sleep(1000);
                    else
                        break;
                }

            });

            DoubleAnimation busyOpacityFadeAnimation = new DoubleAnimation(BusyAnimationUIGrid.Opacity, 0, new Duration(TimeSpan.FromMilliseconds(1500)));
            BusyAnimationUIGrid.BeginAnimation(Grid.OpacityProperty, busyOpacityFadeAnimation);
            busyOpacityFadeAnimation.Completed += (s, e) =>
            {
                BusyAnimationUIGrid.Visibility = Visibility.Hidden;
            };

            DialogResult = true;
            Close();
        }

        private void OnNameOrPathChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }
    }
}
