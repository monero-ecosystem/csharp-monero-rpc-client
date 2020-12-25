using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Utilities
{
    internal class PriceUtilities
    {
        public static decimal PiconeroToMonero(ulong amount)
        {
            decimal piconero = amount;
            const decimal conversion = 1_000_000_000_000;
            return piconero / conversion;
        }

        public static ulong MoneroToPiconero(decimal amount)
        {
            int decimals = GetDecimalPlaces(amount);
            const int decimalPlaces = 12;
            int decimalToAddOrRemove = decimalPlaces - decimals;
            amount = amount * (decimal)Math.Pow(10, decimalToAddOrRemove);
            return checked((ulong)amount);
        }

        private static int GetDecimalPlaces(decimal number)
        {
            int decimalPlaces = 0;
            number = Math.Abs(number);
            number -= (int)number; // Remove the integer part of the number
            while (number > 0)
            {
                decimalPlaces++;
                number *= 10;
                number -= (int)number;
            }
            return decimalPlaces;
        }
    }

    internal class PriceFormat
    {
        public readonly static string TwelveDecimalPlaces = "N12";  
    }

    internal class DateFormat
    {
        public readonly static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fffffff";
    }
}