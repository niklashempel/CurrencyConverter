using System;
using System.Threading.Tasks;
using ConverterApp.Interfaces;

namespace ConverterApp.Business
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafety(this Task task, IErrorHandler handler)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler.HandleError(ex);
            }
        }
    }
}