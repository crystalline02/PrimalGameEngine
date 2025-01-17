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

            if(e.Key == Key.Enter)
            {
                if(!DoRecordedRenaming(textBox))
                    be.UpdateSource();  //  如果没法完成记录撤回重做的命名操作，则直接目标域更新源域，不通过Command更新源域
                Keyboard.ClearFocus();
                //e.Handled = true;  //  防止按下回车触发的OnKeyDown事件处理冒泡路由传播
            }
            else if(e.Key == Key.Escape)
            {
                be.UpdateTarget();  //  源域更新目标域，退回当前输入
                Keyboard.ClearFocus();
            }
        }

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            BindingExpression? be = textBox?.GetBindingExpression(TextBox.TextProperty);

            // 当前TextBox的text属性没有绑定到任何属性，我们不需要失去焦点的处理逻辑，直接让其冒泡路由传播
            if (be == null)
                return;

            if (!DoRecordedRenaming(textBox))
                be.UpdateSource();  //  如果没法完成记录撤回重做的命名操作，则直接目标域更新源域，不通过Command更新源域
        }

        private bool DoRecordedRenaming(TextBox? textBox)
        {
            string? newValue = textBox?.Text;
            ICommand? renameCommmand = textBox?.Tag as ICommand;
            if(renameCommmand != null && renameCommmand.CanExecute(newValue))
            {
                renameCommmand.Execute(newValue);  //如果有Tag作为TextBox的Command，通过Command更新源域，Command会记录撤回重做的操作
                return true;
            }
            return false;
        }
    }
}
