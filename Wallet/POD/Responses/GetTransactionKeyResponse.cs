using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class GetTransactionKeyResponse : RpcResponse
    {
        public GetTransactionKeyResult result { get; set; }
    }

    public class GetTransactionKeyResult
    {
        public string tx_key { get; set; }
    }
}