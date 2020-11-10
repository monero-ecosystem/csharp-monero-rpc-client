using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class AlternateChainResponse : RpcResponse
    {
        public AlternateChainResult result { get; set; }
        public string status { get; set; }
    }

    public class AlternateChainResult
    {
        public List<Chain> chains { get; set; } = new List<Chain>();
    }
}