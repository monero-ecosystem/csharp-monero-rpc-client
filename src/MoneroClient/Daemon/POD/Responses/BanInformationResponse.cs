using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class GetBansResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BanInformation Result { get; set; }
    }
}