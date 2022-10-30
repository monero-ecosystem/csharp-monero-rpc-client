using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class TransactionPool
    {
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("spent_key_images")]
        public List<SpentKeyImage> SpentKeyImages { get; set; } = new List<SpentKeyImage>();
        [JsonPropertyName("transactions")]
        public List<TransactionPoolTransaction> Transactions { get; set; } = new List<TransactionPoolTransaction>();
        [JsonPropertyName("untrusted")]
        public bool Untrusted { get; set; }
        public override string ToString()
        {
            return $"Tx Count: {this.Transactions.Count}";
        }
    }
}