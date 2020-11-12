using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MakeUriResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public MakeUri Result { get; set; }
    }

    public class MakeUri
    {
        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }
}