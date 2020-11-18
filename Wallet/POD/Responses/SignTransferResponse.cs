using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignTransfer Result { get; set; }
    }

    public class SignTransfer
    {
        [JsonPropertyName("signed_txset")]
        public string SignedTransactionSet { get; set; }
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
        [JsonPropertyName("tx_raw_list")]
        public List<string> RawTransactions { get; set; } = new List<string>();
    }
}