using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class ConnectionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ConnectionResult Result { get; set; }
    }
}