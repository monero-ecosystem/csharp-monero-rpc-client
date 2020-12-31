using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class ConnectionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ConnectionResult Result { get; set; }
    }

    internal class ConnectionResult
    {
        [JsonPropertyName("connections")]
        public List<Connection> Connections { get; set; } = new List<Connection>();
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Connections);
        }
    }
}