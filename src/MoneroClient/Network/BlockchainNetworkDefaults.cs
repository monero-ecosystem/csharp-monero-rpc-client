﻿using System;

namespace Monero.Client.Network
{
    internal static class BlockchainNetworkDefaults
    {
        public static readonly TimeSpan AverageBlockTime = TimeSpan.FromMinutes(2);
        public const int BaseBlockUnlockThreshold = 10;
    }
}