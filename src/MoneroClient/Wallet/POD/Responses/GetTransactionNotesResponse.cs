using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionNotesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetTransactionNotesResult Result { get; set; }
    }
}