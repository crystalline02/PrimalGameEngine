using PrimalEditor.Editors;
using PrimalEditor.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PrimalEditor.Dictionaries
{
    partial class ControlTemplates: ResourceDictionary
    {
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            BindingExpression? be = textBox?.GetBindingExpression(TextBox.TextProperty);
            
            // 当前TextBox的text属性没有绑定到任何属性，我们不需要按键按下的处理逻辑，直接让其冒泡路由传播
            if (be == null)
                return;

            // 如果text绑定到某个属性了
            // 检查Tag上有没有Command，如果没有或者执行不了，则不需要处理任何逻辑，直接让其冒泡路由传播
            string? newName = textBox?.Text;
            ICommand? RenameCommand = textBox?.Tag as ICommand;
            if(RenameCommand == null || !RenameCommand.CanExecute(newName)) 
                return;

            //  如果有Command，我们认为Binding mode应该是One way，我们只希望通过Command来更新数据源，并且是显示地按键或者失焦
            if(be?.ParentBinding.Mode != BindingMode.OneWay)
            {
                Logger.Log($"{textBox}' has text binding and command, but is not set to OneWay binding mode,", MessageType.Warning);
                return;
            }

            if(be?.ParentBinding.UpdateSourceTrigger != UpdateSourceTrigger.Explicit)
            {
                Logger.Log($"{textBox}' has text binding and command, but is not set to Explicit UpdateSourceTrigger,", MessageType.Warning);
                return;
            }

            if(e.Key == Key.Enter)
            {
                RenameCommand.Execute(newName);  // 执行RenameCommand就相当于更新数据源了，be.UpdateSource();也就执行了
                textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
                e.Handled = true;  //  防止按下回车触发的OnKeyDown事件处理冒泡路由传播
            }
            else if(e.Key == Key.Escape)
            {
                be.UpdateTarget();  //  源域更新目标域，退回当前输入
                textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
                e.Handled = true;  //  防止按下ESC触发的OnKeyDown事件处理冒泡路由传播
            }
        }

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            BindingExpression? be = textBox?.GetBindingExpression(TextBox.TextProperty);

            // 当前TextBox的text属性没有绑定到任何属性，我们不需要失去焦点的处理逻辑，直接让其冒泡路由传播
            if (be == null)
                return;

            // 如果text绑定到某个属性了，检查Tag上有没有Command，如果没有或者执行不了，则直接更新源域，然后让其冒泡路由传播
            string? newName = textBox?.Text;
            ICommand? RenameCommand = textBox?.Tag as ICommand;
            if (RenameCommand == null || !RenameCommand.CanExecute(newName))
            {
                be.UpdateSource();
                return;
            }

            //  如果有Command，我们认为Binding mode应该是One way，我们只希望通过Command来更新数据源，并且是显示地按键或者失焦
            if (be?.ParentBinding.Mode != BindingMode.OneWay)
            {
                Logger.Log($"{textBox}' has text binding and command, but not set to OneWay binding mode,", MessageType.Warning);
                return;
            }

            RenameCommand.Execute(newName);
        }

        private void OnCloseWindowButtonClicked(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement)?.TemplatedParent as Window)?.Close();
        }

        private void OnChangeWindowStateButtonClicked(object sender, RoutedEventArgs e)
        {
            Window? w = (sender as FrameworkElement)?.TemplatedParent as Window;
            w.WindowState = (w?.WindowState == WindowState.Maximized ? WindowState.Normal :WindowState.Maximized);
        }

        private void OnChangeMinimizeWindowButtonClicked(object sender, RoutedEventArgs e)
        {
            Window? w = (sender as FrameworkElement)?.TemplatedParent as Window;
            w.WindowState = WindowState.Minimized;
        }

        private void OnRenameTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            BindingExpression? be = textBox?.GetBindingExpression(TextBox.TextProperty);

            // 无论如何，首先处理TextBox，按下Enter或Escape都隐藏
            if(e.Key == Key.Enter || e.Key == Key.Escape)
            {
                textBox.Visibility = Visibility.Collapsed;
            }

            // 当前TextBox的text属性没有绑定到任何属性，我们不需要按键按下的处理逻辑，直接让其冒泡路由传播
            if (be == null)
                return;

            // 如果text绑定到某个属性了
            // 检查Tag上有没有Command，如果没有或者执行不了，则不需要处理任何逻辑，直接让其冒泡路由传播
            string? newName = textBox?.Text;
            ICommand? RenameCommand = textBox?.Tag as ICommand;
            if (RenameCommand == null || !RenameCommand.CanExecute(newName))
                return;

            //  如果有Command，我们认为Binding mode应该是One way，我们只希望通过Command来更新数据源，并且是显示地按键或者失焦
            if (be?.ParentBinding.Mode != BindingMode.OneWay)
            {
                Logger.Log($"{textBox}' has text binding and command, but is not set to OneWay binding mode,", MessageType.Warning);
                return;
            }

            if (be?.ParentBinding.UpdateSourceTrigger != UpdateSourceTrigger.Explicit)
            {
                Logger.Log($"{textBox}' has text binding and command, but is not set to Explicit UpdateSourceTrigger,", MessageType.Warning);
                return;
            }

            if (e.Key == Key.Enter)
            {
                RenameCommand.Execute(newName);  // 执行RenameCommand就相当于更新数据源了，be.UpdateSource();也就执行了
                textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
                e.Handled = true;  //  防止按下回车触发的OnKeyDown事件处理冒泡路由传播
            }
            else if (e.Key == Key.Escape)
            {
                be.UpdateTarget();  //  源域更新目标域，退回当前输入
                textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Last));
                e.Handled = true;  //  防止按下ESC触发的OnKeyDown事件处理冒泡路由传播
            }
        }

        private void OnRenameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            BindingExpression? be = textBox?.GetBindingExpression(TextBox.TextProperty);

            // 无论如何，失焦都要隐藏这个textBox
            textBox.Visibility = Visibility.Collapsed;

            // 当前TextBox的text属性没有绑定到任何属性，我们不需要失去焦点的处理逻辑，直接让其冒泡路由传播
            if (be == null)
                return;

            // 如果text绑定到某个属性了，检查Tag上有没有Command，如果没有或者执行不了，则直接更新源域，然后让其冒泡路由传播
            string? newName = textBox?.Text;
            ICommand? RenameCommand = textBox?.Tag as ICommand;
            if (RenameCommand == null || !RenameCommand.CanExecute(newName))
            {
                be.UpdateSource();
                return;
            }

            // 如果有Command，我们认为Binding mode应该是One way，我们只希望通过Command来更新数据源，并且是显示地按键或者失焦
            if (be?.ParentBinding.Mode != BindingMode.OneWay)
            {
                Logger.Log($"{textBox}' has text binding and command, but not set to OneWay binding mode,", MessageType.Warning);
                return;
            }

            RenameCommand.Execute(newName);
        }
    }
}
