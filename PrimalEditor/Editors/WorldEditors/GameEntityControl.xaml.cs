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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimalEditor.Editors
{
    /// <summary>
    /// Interaction logic for GameEntityControl.xaml
    /// </summary>
    public partial class GameEntityControl : UserControl
    {
        public static GameEntityControl? Instance;

        public GameEntityControl()
        {
            InitializeComponent();

            Instance = this;
            DataContext = null;  // GameEntityControl的DataContext取决于我们在ProjectLayoutControl中选择的GameEntity，这里暂时设置为null
        }
    }
}
