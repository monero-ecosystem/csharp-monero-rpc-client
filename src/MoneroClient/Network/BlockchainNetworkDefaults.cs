using System;

namespace Monero.Client.Network
{
    internal static class BlockchainNetworkDefaults
    {
        public const int BaseBlockUnlockThreshold = 10;
        public static readonly TimeSpan AverageBlockTime = TimeSpan.FromMinutes(2);
    }
}