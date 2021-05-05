using System;

namespace Monero.Client.Network
{
    internal static class MoneroNetworkDefaults
    {
        public const string DaemonMainnetUrl = @"127.0.0.1";
        public const uint DaemonMainnetPort = 18081;
        public const string DaemonStagenetUrl = @"127.0.0.1";
        public const uint DaemonStagenetPort = 38081;
        public const string DaemonTestnetUrl = @"127.0.0.1";
        public const uint DaemonTestnetPort = 28081;
        public const string WalletMainnetUrl = @"127.0.0.1";
        public const uint WalletMainnetPort = 18082;
        public const string WalletStagenetUrl = @"127.0.0.1";
        public const uint WalletStagenetPort = 38082;
        public const string WalletTestnetUrl = @"127.0.0.1";
        public const uint WalletTestnetPort = 28082;
    }

    internal static class FieldAndHeaderDefaults
    {
        public const string ApplicationJson = "application/json";
        public const string JsonRpc = "2.0";
        public const string Id = "0";
        public const string CharsetUtf8 = "utf-8";
        public const string CharsetUtf16 = "utf-16";
    }

    internal static class BlockchainNetworkDefaults
    {
        public readonly static TimeSpan AverageBlockTime = TimeSpan.FromMinutes(2);
        public const int BaseBlockUnlockThreshold = 10;
    }
}