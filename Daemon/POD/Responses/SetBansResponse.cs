using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class SetBansResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SetBansResult Result { get; set; }
    }

    public class SetBansResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

}