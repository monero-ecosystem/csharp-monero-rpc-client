using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockHeaderResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockHeaderResult Result { get; set; }
    }

    public class BlockHeaderResult
    {
        [JsonPropertyName("block_header")]
        public BlockHeader BlockHeader { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        public override string ToString()
        {
            return $"{BlockHeader}";
        }
    }
}