using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class TransactionPoolBacklogResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public TransactionPoolBacklog Result { get; set; }
    }

    public class TransactionPoolBacklog
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
        public bool IsUntrusted { get; set; }
        public override string ToString()
        {
            return $"{TopHash} - {Status} - {(IsUntrusted ? "Untrusted" : "Trusted")}";
        }
    }
}