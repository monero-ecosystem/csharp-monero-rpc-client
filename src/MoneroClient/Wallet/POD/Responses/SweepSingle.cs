using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    public class SweepSingle
    {
        [JsonPropertyName("tx_hash")]
        public string TxHash { get; set; }
        [JsonPropertyName("tx_key")]
        public string TxKey { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("fee")]
        public ulong Fee { get; set; }
        [JsonPropertyName("weight")]
        public ulong Weight { get; set; }
        [JsonPropertyName("tx_blob")]
        public string TxBlob { get; set; }
        [JsonPropertyName("tx_metadata")]
        public string TxMetaData { get; set; }
        [JsonPropertyName("multisig_txset")]
        public string MultiSigTxSet { get; set; }
        [JsonPropertyName("unsigned_txset")]
        public string UnsignedTxSet { get; set; }
    }
}