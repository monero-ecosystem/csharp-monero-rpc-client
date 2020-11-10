using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class TransactionPoolBacklogResponse : RpcResponse
    {
        public TransactionPoolBacklogResult result { get; set; }
    }

    public class TransactionPoolBacklogResult
    {      
        public string backlog { get; set; }
        public uint credits { get; set; }
        public string status { get; set; }
        public string top_hash { get; set; }
        public bool untrusted { get; set; }
    }
}