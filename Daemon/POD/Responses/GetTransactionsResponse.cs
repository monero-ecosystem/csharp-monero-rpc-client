using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    public class TransactionSet
    {
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("txs")]
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        [JsonPropertyName("txs_as_hex")]
        public List<string> TxsAsHex { get; set; } = new List<string>();
        [JsonPropertyName("untrusted")]
        public bool Untrusted { get; set; }
        public override string ToString()
        {
            return $"Tx Count: {Transactions.Count}";
        }
    }
}