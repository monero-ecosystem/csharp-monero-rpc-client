using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
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
            return $"{this.TopHash} - {this.Status} - {(this.IsUntrusted ? "Untrusted" : "Trusted")}";
        }
    }
}