using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignTransferResult Result { get; set; }
    }

    public class SignTransferResult
    {
        [JsonPropertyName("signed_txset")]
        public string SignedTransactionSet { get; set; }
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
        [JsonPropertyName("tx_raw_list")]
        public List<string> RawTransactions { get; set; } = new List<string>();
    }
}