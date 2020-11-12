using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class RelayTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public RelayTransactionResult Result { get; set; }
    }

    public class RelayTransactionResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}