using System;
using System.Linq;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class HardforkInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public HardforkInformation Result { get; set; }
    }
}