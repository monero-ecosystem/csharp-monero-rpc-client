using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("MoneroClient.UnitTests")]
namespace Monero.Client.Utilities
{

    internal class PriceUtilities
    {
        private const int LowestBase = 12;
        private const decimal Piconero = 0.000000000001M;
        private const decimal Nanonero = 0.000000001M;
        private const decimal Micronero = 0.000001M;
        private const decimal Millinero = 0.001M;
        private const decimal Centinero = 0.01M;
        private const decimal Decinero = 0.1M;
        private const decimal Monero = 1M;
        private const decimal Decanero = 10M;
        private const decimal Hectonero = 100M;
        private const decimal Kilonero = 1000M;
        private const decimal Meganero = 1000000M;

        public static decimal PiconeroToMonero(ulong amount)
        {
            decimal piconero = amount;
            const decimal conversion = 1_000_000_000_000;
            return piconero / conversion;
        }

        public static ulong MoneroToPiconero(decimal amount)
        {
            if (amount < decimal.Zero)
                throw new InvalidOperationException("Cannot have a negative amount of Monero");
            int decimalPlaces = GetDecimalPlaces(amount);
            if (decimalPlaces > LowestBase)
                throw new InvalidOperationException($"{amount} has more than {LowestBase} decimal places. " +
                    $"{amount} can only have 12 decimal places at most.");
            amount = amount * (decimal)Math.Pow(10, LowestBase);
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
}