using System;

namespace Monero.Client.Network
{
    internal static class MoneroNetworkDefaults
    {
        public const string DaemonMainnetUri = @"http://127.0.0.1:18081/json_rpc";
        public const string DaemonStagenetUri = @"http://127.0.0.1:38081/json_rpc";
        public const string DaemonTestnetUri = @"http://127.0.0.1:28081/json_rpc";
        public const string WalletMainnetUri = @"http://127.0.0.1:18082/json_rpc";
        public const string WalletStagenetUri = @"http://127.0.0.1:38082/json_rpc";
        public const string WalletTestnetUri = @"http://127.0.0.1:28082/json_rpc";
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