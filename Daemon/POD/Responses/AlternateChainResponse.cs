using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class AlternateChainResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AlternateChainResult Result { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class AlternateChainResult
    {
        [JsonPropertyName("chains")]
        public List<Chain> Chains { get; set; } = new List<Chain>();
    }
}