using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockHeaderRangeResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockHeaderRangeResult Result { get; set; }
    }
}