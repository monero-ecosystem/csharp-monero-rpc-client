using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitMultiSigTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitMultiSig Result { get; set; }
    }

    public class SubmitMultiSig
    {
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
    }
}