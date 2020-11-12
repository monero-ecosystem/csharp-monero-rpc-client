using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class FlushTransactionPoolResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FlushTransactionPoolResult Result { get; set; }
    }

    public class FlushTransactionPoolResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}