using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitTransfer Result { get; set; }
    }

    public class SubmitTransfer
    {
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
    }
}