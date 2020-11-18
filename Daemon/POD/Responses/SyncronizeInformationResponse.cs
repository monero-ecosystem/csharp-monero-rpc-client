using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class SyncronizeInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SyncronizationInformation Result { get; set; }
    }

    public class SyncronizationInformation
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("target_height")]
        public ulong TargetHeight { get; set; }
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("overview")]
        public string Overview { get; set; }
        [JsonPropertyName("peers")]
        public List<GeneralSyncronizationInformation> Peers { get; set; } = new List<GeneralSyncronizationInformation>();
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("next_needed_pruning_seed")]
        public uint NextNeededPruningSeed { get; set; }
    }

    public class GeneralSyncronizationInformation
    {
        [JsonPropertyName("info")]
        public Connection Connection { get; set; }
    }
}