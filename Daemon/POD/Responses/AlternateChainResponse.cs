using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
{
    public class AlternateChainResponse : RpcResponse
    {
        public AlternateChainResult result { get; set; }
        public string status { get; set; }
    }

    public class AlternateChainResult
    {
        public List<Chain> chains { get; set; } = new List<Chain>();
    }
}