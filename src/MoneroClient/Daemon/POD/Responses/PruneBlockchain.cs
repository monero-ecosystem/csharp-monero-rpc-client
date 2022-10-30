using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{

    public class PruneBlockchain
    {
        [JsonPropertyName("pruned")]
        public bool IsPruned { get; set; }
        [JsonPropertyName("pruning_seed")]
        public uint PruningSeed { get; set; }
    }
}