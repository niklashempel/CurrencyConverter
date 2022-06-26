using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ConverterApp.Interfaces;

namespace ConverterApp.Business
{
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<bool> canExecute;

        private readonly IErrorHandler errorHandler;

        private readonly Func<Task> execute;

        private bool isExecuting;

        public event EventHandler CanExecuteChanged;

        public AsyncCommand(
            Func<Task> execute,
            Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            this.errorHandler = ErrorHandler.Create();
        }

        public bool CanExecute()
        {
            return !isExecuting && (canExecute?.Invoke() ?? true);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().FireAndForgetSafety(errorHandler);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    isExecuting = true;
                    await execute();
                }
                finally
                {
                    isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}