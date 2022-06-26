using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConverterApp.Interfaces
{
    public interface IAsyncCommand : ICommand
    {
        bool CanExecute();

        Task ExecuteAsync();
    }
}