using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class TransactionPoolBacklogResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public TransactionPoolBacklogResult Result { get; set; }
    }

    public class TransactionPoolBacklogResult
    {      
        [JsonPropertyName("backlog")]
        public string Backlog { get; set; }
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("untrusted")]
        public bool Untrusted { get; set; }
    }
}