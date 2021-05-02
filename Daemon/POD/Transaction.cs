using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class Transaction
    {
        [JsonPropertyName("as_hex")]
        public string AsHex { get; set; }
        [JsonPropertyName("as_json")]
        public string AsJson { get; set; }
        [JsonPropertyName("prunable_as_hex")]
        public string PrunableAsHex { get; set; }
        [JsonPropertyName("prunable_hash")]
        public string PrunableHash { get; set; }
        [JsonPropertyName("pruned_as_hex")]
        public string PrunedAsHex { get; set; }
        [JsonPropertyName("double_spend_seen")]
        public bool IsDoubleSpendSeen { get; set; }
        [JsonPropertyName("in_pool")]
        public bool InPool { get; set; }
        [JsonPropertyName("tx_hash")]
        public string TxHash { get; set; }
        [JsonPropertyName("block_height")]
        public ulong Height { get; set; }
        [JsonPropertyName("block_timestamp")]
        public ulong Timestamp { get; set; }
        [JsonIgnore()]
        public DateTime DateTime
        {
            get
            {
                return DateTime.UnixEpoch.AddSeconds(this.Timestamp);
            }
        }
        public override string ToString()
        {
            return TxHash;
        }
    }
}