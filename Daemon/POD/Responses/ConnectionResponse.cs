using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class ConnectionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ConnectionResult Result { get; set; }
    }

    public class ConnectionResult
    {
        [JsonPropertyName("connections")]
        public List<Connection> Connections { get; set; } = new List<Connection>();
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool Untrusted { get; set; }
    }
}