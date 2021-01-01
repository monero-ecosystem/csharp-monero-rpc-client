using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class PruneBlockchainResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public PruneBlockchain Result { get; set; }
    }

    public class PruneBlockchain
    {
        [JsonPropertyName("pruned")]
        public bool IsPruned { get; set; }
        [JsonPropertyName("pruning_seed")]
        public uint PruningSeed { get; set; }
    }
}