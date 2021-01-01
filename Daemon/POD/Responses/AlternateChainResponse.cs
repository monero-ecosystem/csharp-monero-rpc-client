using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class AlternateChainResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AlternateChainResult Result { get; set; }
    }

    internal class AlternateChainResult
    {
        [JsonPropertyName("chains")]
        public List<Chain> Chains { get; set; } = new List<Chain>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Chains);
        }
    }
}