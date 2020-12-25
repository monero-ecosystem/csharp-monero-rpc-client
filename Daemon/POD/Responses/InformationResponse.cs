using Monero.Client.Network;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class DaemonInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DaemonInformation Result { get; set; }

    }

    public class DaemonInformation
    {
        [JsonPropertyName("adjusted_time")]
        public ulong AdjustedTime { get; set; }
        [JsonPropertyName("alt_blocks_count")]
        public ulong AltBlocksCount { get; set; }
        [JsonPropertyName("block_size_limit")]
        public ulong BlockSizeLimit { get; set; }
        [JsonPropertyName("block_size_median")]
        public ulong BlockSizeMedian { get; set; }
        [JsonPropertyName("block_weight_limit")]
        public ulong BlockWeightLimit { get; set; }
        [JsonPropertyName("block_weight_median")]
        public ulong BlockWeightMedian { get; set; }
        [JsonPropertyName("bootstrap_daemon_address")]
        public string BootstrapDaemonAddress { get; set; }
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("cumulative_difficulty")]
        public ulong CumulativeDifficulty { get; set; }
        [JsonPropertyName("cumulative_difficulty_top64")]
        public ulong CumulativeDifficultyTop64 { get; set; }
        [JsonPropertyName("database_size")]
        public ulong DatabaseSize { get; set; }
        [JsonPropertyName("difficulty")]
        public ulong Difficulty { get; set; }
        [JsonPropertyName("difficulty_top64")]
        public ulong DifficultyTop64 { get; set; }
        [JsonPropertyName("free_space")]
        public ulong FreeSpace { get; set; }
        [JsonPropertyName("grey_peerlist_size")]
        public ulong GreyPeerlistSize { get; set; }
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("height_without_bootstrap")]
        public ulong HeightWithoutBootstrap { get; set; }
        [JsonPropertyName("incoming_connections_count")]
        public ulong IncomingConnectionsCount { get; set; }
        [JsonPropertyName("mainnet")]
        public bool IsMainnet { get; set; }
        [JsonPropertyName("nettype")]
        public string NetType { get; set; }
        [JsonPropertyName("offline")]
        public bool IsOffline { get; set; }
        [JsonPropertyName("outgoing_connections_count")]
        public ulong OutgoingConnectionsCount { get; set; }
        [JsonPropertyName("rpc_connections_count")]
        public ulong RpcConnectionsCount { get; set; }
        [JsonPropertyName("stagenet")]
        public bool IsStagenet { get; set; }
        [JsonPropertyName("start_time")]
        public ulong StartTime { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("target")]
        public ulong Target { get; set; }
        [JsonPropertyName("target_height")]
        public ulong TargetHeight { get; set; }
        [JsonPropertyName("testnet")]
        public bool IsTestnet { get; set; }
        [JsonPropertyName("top_block_hash")]
        public string TopBlockHash { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("tx_count")]
        public ulong TxCount { get; set; }
        [JsonPropertyName("tx_pool_size")]
        public ulong TxPoolSize { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("update_available")]
        public bool IsUpdateAvailable { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("was_bootstrap_ever_used")]
        public bool WasBootstrapEverUsed { get; set; }
        [JsonPropertyName("white_peerlist_size")]
        public ulong WhitePeerlistSize { get; set; }
        [JsonPropertyName("wide_cumulative_difficulty")]
        public string WideCumulativeDifficulty { get; set; }
        [JsonPropertyName("wide_difficulty")]
        public string WideDifficulty { get; set; }
        [JsonIgnore()]
        public MoneroNetwork NetworkType
        {
            get
            {
                if (this.IsMainnet)
                    return MoneroNetwork.Mainnet;
                else if (this.IsStagenet)
                    return MoneroNetwork.Stagenet;
                else if (this.IsTestnet)
                    return MoneroNetwork.Testnet;
                else
                    throw new InvalidOperationException("Unknown network type");
            }
        }
        public override string ToString()
        {
            var typeInfo = typeof(DaemonInformation);
            var nonNullPropertyList = typeInfo.GetProperties()
                                              .Where(p => p.GetValue(this) != default)
                                              .Select(p => $"{p.Name}: {p.GetValue(this)} ");
            return string.Join(Environment.NewLine, nonNullPropertyList);
        }
    }
}