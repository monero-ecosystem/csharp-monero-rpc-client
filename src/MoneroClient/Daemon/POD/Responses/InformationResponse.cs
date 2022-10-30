using System;
using System.Linq;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class DaemonInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DaemonInformation Result { get; set; }
    }
}