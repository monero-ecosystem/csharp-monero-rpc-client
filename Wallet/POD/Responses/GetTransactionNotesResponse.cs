using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionNotesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetTransactionNotesResult Result { get; set; }
    }

    internal class GetTransactionNotesResult
    {
        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; } = new List<string>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Notes);
        }
    }
}