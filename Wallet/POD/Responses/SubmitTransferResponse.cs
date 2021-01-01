using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitTransferResult Result { get; set; }
    }

    internal class SubmitTransferResult
    {
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, TransactionHashes);
        }
    }
}