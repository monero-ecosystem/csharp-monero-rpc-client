using System;
using System.Linq;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class GetBlockTemplateResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockTemplate Result { get; set; }
    }
}