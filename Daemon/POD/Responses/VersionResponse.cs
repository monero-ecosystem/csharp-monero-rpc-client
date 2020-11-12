using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class VersionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DaemonVersion Result { get; set; }
    }

    public class DaemonVersion
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool Untrusted { get; set; }
        [JsonPropertyName("version")]
        public uint Version { get; set; }
    }
}