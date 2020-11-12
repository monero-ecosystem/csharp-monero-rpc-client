using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignMultiSigTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignMultiSigTransactionResult Result { get; set; }
    }

    public class SignMultiSigTransactionResult
    {
        [JsonPropertyName("tx_data_hex")]
        public string TransactionDataHex { get; set; }
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
    }
}