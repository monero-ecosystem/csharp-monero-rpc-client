using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    public class SignTransfer
    {
        [JsonPropertyName("signed_txset")]
        public string SignedTransactionSet { get; set; }
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
        [JsonPropertyName("tx_raw_list")]
        public List<string> RawTransactions { get; set; } = new List<string>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.TransactionHashes);
        }
    }
}