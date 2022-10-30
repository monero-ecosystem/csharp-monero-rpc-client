using System;
using System.Text;
using Monero.Client.Network;

namespace Monero.Client.Utilities
{

    internal class ErrorThresholds
    {
        /// <summary>
        /// There are 365 days in a year. 
        /// I will not let someone lock their funds for longer than a year.
        /// I do not want people making mistakes, and blaming me. 
        /// For example, 0ul - 1 is ulong.MaxValue, which is eternity to have ones' funds locked for.
        /// </summary>
        public static readonly ulong MaximumUnlockTime = (ulong)(TimeSpan.FromDays(365) / BlockchainNetworkDefaults.AverageBlockTime);
    }
}