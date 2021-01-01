using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class SubmitBlockResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitBlock Result { get; set; }
    }

    public class SubmitBlock
    {
        // ...
    }
}