using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
{
    public class RelayTransactionResponse : RpcResponse
    {
        public RelayTransactionResult result { get; set; }
    }

    public class RelayTransactionResult
    {
        public string status { get; set; }
    }
}