using System;
using ConverterApp.Interfaces;

namespace ConverterApp.Business
{
    public class ErrorHandler : IErrorHandler
    {
        public static IErrorHandler Create()
        {
            return new ErrorHandler();
        }

        public void HandleError(Exception ex)
        {
            // Handle the error here. For example with a message box.
            Console.WriteLine(ex.Message);
        }
    }
}