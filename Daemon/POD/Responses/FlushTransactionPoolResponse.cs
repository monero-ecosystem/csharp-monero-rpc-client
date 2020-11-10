using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class FlushTransactionPoolResponse : RpcResponse
    {
        public FlushTransactionPoolResult result { get; set; }
    }

    public class FlushTransactionPoolResult
    {
        public string status { get; set; }
    }
}