using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class AlternateChainResult
    {
        [JsonPropertyName("chains")]
        public List<Chain> Chains { get; set; } = new List<Chain>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.Chains);
        }
    }
}