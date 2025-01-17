using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrimalEditor
{
    /*模板参数T表示CommandParamter的类型*/
    internal class RelayCommand<T> : ICommand
    {
        // 每一个Command的CanExecuteChanged事件都会在主窗口InitializeComponent()的时候自动添加一个OnCanExecuteChanged函数，这个函数调用CanExecute方法
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<T> _execute;
        private Predicate<T> _canExecute;

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke((T)parameter);
        }

        public RelayCommand(Action<T>? execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
    }
}
