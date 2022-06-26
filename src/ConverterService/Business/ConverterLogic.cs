using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterService.Business
{
    public class ConverterLogic
    {
        private static readonly double MaximumValue = 999999999.99;
        private static readonly double MinimumValue = 0;

        private static readonly string[] TenWords = new string[]
                {
            "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
        };

        private static readonly string[] WordsUpToTwenty = new string[] {
            "one", "two", "three", "four", "five",
            "six", "seven", "eight", "nine", "ten", "eleven",
            "twelve", "thirteen", "fourteen", "fifteen",
            "sixteen", "seventeen", "eighteen", "nineteen"};

        public string ConvertNumberToWord(double number)
        {
            if (number < MinimumValue)
            {
                throw new ArgumentException($"The number must be greater than or equal to {MinimumValue}.");
            }

            if (number > MaximumValue)
            {
                throw new ArgumentException($"The number must be less than or equal to {MaximumValue}");
            }

            if (!Math.Round(number, 2).Equals(number))
            {
                throw new FormatException($"The number can only have up to two decimal places.");
            }

            var wholePart = (int)Math.Truncate(number);
            var decimalPart = (int)((number - wholePart) * 100);

            var dollars = wholePart == 0 ? "zero" : ConvertNumberToMoney(wholePart);
            dollars = AddCurrencyWord(dollars, "dollar", wholePart);

            var cents = ConvertNumberToMoney(decimalPart);
            cents = AddCurrencyWord(cents, "cent", decimalPart);

            if (decimalPart > 0)
            {
                return $"{dollars} and {cents}";
            }
            else
            {
                return $"{dollars}";
            }
        }

        private string AddCurrencyWord(string word, string currency, int number)
        {
            return number == 1 ? $"{word} {currency}" : $"{word} {currency}s";
        }

        private string ConvertNumberToMoney(int number)
        {
            var result = string.Empty;
            if (number >= 1000000)
            {
                var digitGroup = number / 1000000;
                result = $"{ConvertNumberToMoney(digitGroup)} million {ConvertNumberToMoney(number - (digitGroup * 1000000))}";
            }
            else if (number >= 1000)
            {
                var digitGroup = number / 1000;
                result = $"{ConvertNumberToMoney(digitGroup)} thousand {ConvertNumberToMoney(number - (digitGroup * 1000))}";
            }
            else if (number >= 100)
            {
                var digitGroup = number / 100;
                return $"{WordsUpToTwenty[digitGroup - 1]} hundred {ConvertNumberToMoney(number - (digitGroup * 100))}";
            }
            else if (number >= 20)
            {
                var tens = number / 10;
                var ones = number % 10;

                result = ones == 0 ? $"{TenWords[tens - 2]}" : $"{TenWords[tens - 2]}-{WordsUpToTwenty[ones - 1]}";
            }
            else if (number > 0)
            {
                result = $"{WordsUpToTwenty[number - 1]}";
            }

            return result.TrimEnd();
        }
    }
}