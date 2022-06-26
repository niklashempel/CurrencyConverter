using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.Constants;
using ConverterApp.Business;

namespace ConverterApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private static double MaximumValue = IntegerConstants.MaximumValue;
        private static double MinimumValue = IntegerConstants.MinimumValue;
        private readonly ApiClient client;
        private AsyncCommand convertCurrencyAsyncCommand;
        private string enteredText = string.Empty;

        private string responseText;

        public AsyncCommand ConvertCurrencyAsyncCommand
        {
            get
            {
                return convertCurrencyAsyncCommand ??= new AsyncCommand(this.ConvertCurrencyAsync, this.TryParseCurrency);
            }
        }

        public string EnteredText
        {
            get
            {
                return enteredText;
            }
            set
            {
                if (value != enteredText)
                {
                    enteredText = value;
                    convertCurrencyAsyncCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged(nameof(EnteredText));
                }
            }
        }

        public string ResponseText
        {
            get
            {
                return responseText;
            }
            set
            {
                if (value != responseText)
                {
                    responseText = value;
                    OnPropertyChanged(nameof(ResponseText));
                }
            }
        }

        public MainViewModel(ApiClient client)
        {
            this.client = client;
        }

        private async Task ConvertCurrencyAsync()
        {
            if (TryParseCurrency(out var number))
            {
                try
                {
                    ResponseText = (await client.ConvertNumberToWordAsync(number)).Word;
                }
                catch (Exception ex)
                {
                    ResponseText = $"An error occured. {ex.Message}";
                }
            }
        }

        private bool TryParseCurrency()
        {
            return TryParseCurrency(out var _);
        }

        private bool TryParseCurrency(out double number)
        {
            var text = enteredText.Replace(" ", string.Empty);

            return double.TryParse(text, System.Globalization.NumberStyles.Number, CultureInfo.GetCultureInfo("de-DE"), out number)
                && Math.Round(number, 2) == number
                && number >= MinimumValue
                && number <= MaximumValue;
        }
    }
}