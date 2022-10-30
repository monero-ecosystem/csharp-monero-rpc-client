using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    public class SignMultiSigTransaction
    {
        [JsonPropertyName("tx_data_hex")]
        public string TransactionDataHex { get; set; }
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
        public override string ToString()
        {
            return $"DataHex: {this.TransactionDataHex} - TxHashes: {string.Join(" ", this.TransactionHashes)}";
        }
    }
}