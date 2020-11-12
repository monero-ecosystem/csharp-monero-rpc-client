using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionKeyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetTransactionKey Result { get; set; }
    }

    public class GetTransactionKey
    {
        [JsonPropertyName("tx_key")]
        public string TransactionKey { get; set; }
    }
}