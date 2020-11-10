using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionKeyResponse : RpcResponse
    {
        public GetTransactionKeyResult result { get; set; }
    }

    public class GetTransactionKeyResult
    {
        public string tx_key { get; set; }
    }
}