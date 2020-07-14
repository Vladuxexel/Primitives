using System;
using System.Windows.Input;

namespace Primitives
{
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Возможность выполнения
        /// </summary>
        /// <param name="parameter"> Параметр. </param>
        /// <returns> Успешность операции. </returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Событие выполнения.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Выполнение.
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);
    }
}
