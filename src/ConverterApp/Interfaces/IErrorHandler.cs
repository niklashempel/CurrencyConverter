using System;

namespace ConverterApp.Interfaces
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}