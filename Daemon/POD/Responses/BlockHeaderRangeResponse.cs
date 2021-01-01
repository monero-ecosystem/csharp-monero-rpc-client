using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockHeaderRangeResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockHeaderRangeResult Result { get; set; }
    }

    internal class BlockHeaderRangeResult
    {
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("headers")]
        public List<BlockHeader> Headers { get; set; } = new List<BlockHeader>();
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Headers);
        }
    }
}