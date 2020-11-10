using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class RelayTransactionResponse : RpcResponse
    {
        public RelayTransactionResult result { get; set; }
    }

    public class RelayTransactionResult
    {
        public string status { get; set; }
    }
}