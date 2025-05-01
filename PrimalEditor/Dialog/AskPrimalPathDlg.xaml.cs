using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace PrimalEditor
{
    /// <summary>
    /// Interaction logic for AskPrimalPathDlg.xaml
    /// </summary>
    public partial class AskPrimalPathDlg : Window
    {
        public string PrimalEnginePath { get; set; } = string.Empty;

        public AskPrimalPathDlg()
        {
            InitializeComponent();
        }

        private void OnConfirmBtnClicked(object sender, RoutedEventArgs e)
        {
            string enteredPath = PathTextBox.Text;
            if(string.IsNullOrEmpty(enteredPath) )
            {
                ErrorMessage.Text = "Please enter a path";
            }
            else if(enteredPath.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)
            {
                ErrorMessage.Text = "Please enter a valid path";
            }
            else if(!Directory.Exists(enteredPath))
            {
                ErrorMessage.Text = "Path doesn't exists!";
            }
            else
            {
                ErrorMessage.Text = string.Empty;
                DialogResult = true;
                Debug.Assert(Directory.Exists(System.IO.Path.Combine(enteredPath, @"Engine\EngineAPI")));
                PrimalEnginePath = enteredPath;
            }
        }
    }
}
