using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
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
            return string.Join(Environment.NewLine, this.Connections);
        }
    }
}