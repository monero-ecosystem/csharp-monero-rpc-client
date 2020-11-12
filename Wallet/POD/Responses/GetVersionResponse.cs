using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetRpcVersionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetVersion Result { get; set; }
    }

    public class GetVersion
    {
        [JsonPropertyName("version")]
        public uint Version { get; set; }
    }
}