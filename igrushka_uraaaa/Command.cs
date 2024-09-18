using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace igrushka_uraaaa
{
    public class Command(Action<object?> action, Predicate<object?> canExecute = null) : ICommand
    {
        /*private Action<object?> _action;
        private Predicate<object?> _canExecute;
        public Command(Action<object?> action, Predicate<object?> canExecute = null)
        {
             _action = action;
            _canExecute = canExecute;
        }*/
        public bool CanExecute(object? parameter)
        {
            return canExecute?.Invoke(parameter) ?? true; // проверяем возможность вызова
        }

        public void Execute(object? parameter)
        {
            action(parameter); // вызываем
        }
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

    }
}
