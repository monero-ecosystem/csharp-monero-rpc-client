using Monero.Client.Network;
using System.Text.Json.Serialization;

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
        public bool IsUntrusted { get; set; }
        public override string ToString()
        {
            return $"{Count}";
        }
    }
}