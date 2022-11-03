using Monero.Client.Constants;
using System;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class TransactionPoolTransaction
    {
        [JsonPropertyName("blob_size")]
        public ulong BlobSize { get; set; }
        [JsonPropertyName("double_spend_seen")]
        public bool IsDoubleSpendSeen { get; set; }
        [JsonPropertyName("do_not_relay")]
        public bool DoNotRelay { get; set; }
        [JsonPropertyName("fee")]
        public ulong Fee { get; set; }
        [JsonPropertyName("id_hash")]
        public string TxHash { get; set; }
        [JsonPropertyName("kept_by_block")]
        public bool KeptByBlock { get; set; }
        [JsonPropertyName("last_failed_height")]
        public ulong LastFailedHeight { get; set; }
        [JsonIgnore]
        public bool PreviouslyFailed
        {
            get
            {
                return this.LastFailedHeight != BlockchainDefaults.DefaultHeight &&
                    string.Compare(BlockchainDefaults.DefaultIdHash, this.LastFailedTxHash) != 0;
            }
        }

        [JsonPropertyName("last_failed_id_hash")]
        public string LastFailedTxHash { get; set; }
        [JsonPropertyName("last_relayed_time")]
        public ulong LastRelayedTime { get; set; }
        [JsonIgnore]
        public DateTime LastRelayDateTime
        {
            get
            {
                return DateTime.UnixEpoch.AddSeconds(this.LastRelayedTime);
            }
        }

        [JsonPropertyName("max_used_block_height")]
        public ulong MaxUsedBlockHeight { get; set; }
        [JsonPropertyName("max_used_block_id_hash")]
        public string MaxUsedBlockTxHash { get; set; }
        [JsonPropertyName("receive_time")]
        public ulong ReceiveTime { get; set; }
        [JsonIgnore]
        public DateTime ReceiveDateTime
        {
            get
            {
                return DateTime.UnixEpoch.AddSeconds(this.ReceiveTime);
            }
        }

        [JsonPropertyName("relayed")]
        public bool Relayed { get; set; }
        [JsonPropertyName("tx_blob")]
        public string TxBlob { get; set; }
        [JsonPropertyName("tx_json")]
        public string TxJson { get; set; }
        [JsonPropertyName("weight")]
        public ulong Weight { get; set; }
        public override string ToString()
        {
            return this.TxHash;
        }
    }
}