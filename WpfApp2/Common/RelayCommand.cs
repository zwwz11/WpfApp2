using Syncfusion.UI.Xaml.NavigationDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp2.Common
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public Action? doWork;

        public RelayCommand(Action? doWork)
        {
            this.doWork = doWork;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            doWork?.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public Action<T>? doWork;
        
        public RelayCommand(Action<T>? doWork)
        {
            this.doWork = doWork;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            doWork?.Invoke((T)parameter!);
        }
    }
}
