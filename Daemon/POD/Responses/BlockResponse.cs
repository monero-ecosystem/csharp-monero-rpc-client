using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Block Result { get; set; }
    }
}