using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimalEditor.Utilities
{
    /// <summary>
    /// Interaction logic for LoggerControl.xaml
    /// </summary>
    public partial class LoggerControl : UserControl
    {
        public LoggerControl()
        {
            InitializeComponent();
        }

        private void OnClearMessageButtonClicked(object sender, RoutedEventArgs e)
        {
            Logger.Clear();
        }

        private void OnFilterLoggerMessageToggleButtonClicked(object sender, RoutedEventArgs e)
        {
            int filterMask = 0x0;
            if(FilterInfoBtn.IsChecked.Value)
            {
                filterMask |= (int)MessageType.Info;
            }

            if(FilterWarningBtn.IsChecked.Value)
            {
                filterMask |= (int)MessageType.Warning;
            }
            
            if(FilterErrorBtn.IsChecked.Value)
            {
                filterMask |= (int)MessageType.Error;
            }

            Logger.MessageMask = filterMask;
        }
    }
}
