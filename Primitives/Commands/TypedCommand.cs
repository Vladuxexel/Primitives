using System;
using System.Windows;

namespace Primitives.Commands
{
    public abstract class TypedCommand<T> : BaseCommand
    {
        /// <summary>
        /// Указывает, доступна ли возможность вызова команды.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        /// <returns> True - доступна. </returns>
        public sealed override bool CanExecute(object parameter)
        {
            if (parameter is T typedParameter)
                return CanExecute(typedParameter);

            return true;
        }

        /// <summary>
        /// Указывает, доступна ли возможность вызова команды с типизированным параметром.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        /// <returns> True - доступна. </returns>
        protected virtual bool CanExecute(T parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполнение команды.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        public sealed override void Execute(object parameter)
        {
            try
            {
                if (parameter is T typedParameter)
                    Execute(typedParameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка", ex.Message);
            }
        }

        /// <summary>
        /// Выполнение комманды с типизированным параметром.
        /// </summary>
        /// <param name="parameter"> Параметр для команды. </param>
        protected abstract void Execute(T parameter);
    }
}
