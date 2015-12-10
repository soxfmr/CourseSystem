using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Input;

namespace CommonLibrary.Domain
{
    public class ActionCommand : ICommand
    {
        private Action<object> executor;
        private Predicate<object> validator;

        private Dispatcher invoker;

        public ActionCommand(Action<object> exectuor) : this(exectuor, null, null) { }

        public ActionCommand(Action<object> executor, Dispatcher invoker) : this(executor, null, invoker) {}

        public ActionCommand(Action<object> executor, Predicate<object> validator) : this(executor, validator, null) { }

        public ActionCommand(Action<object> executor, Predicate<object> validator, Dispatcher invoker)
        {
            this.executor = executor;
            this.validator = validator;
            this.invoker = invoker;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (validator == null)
            {
                return true;
            }

            return validator(parameter);
        }

        public void Execute(object parameter)
        {
            if (invoker != null)
            {
                invoker.BeginInvoke(executor, parameter);
                return;
            }

            executor(parameter);
        }

        public void Execute()
        {
            Execute(null);
        }
    }
}
