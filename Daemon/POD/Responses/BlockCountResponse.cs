using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockCountResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockCountResult Result { get; set; }
    }

    public class BlockCountResult
    {
        [JsonPropertyName("count")]
        public ulong Count { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool Untrusted { get; set; }
    }
}