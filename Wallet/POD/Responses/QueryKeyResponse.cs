using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class QueryKeyResponse : RpcResponse
    {
        public QueryKeyResult result { get; set; }
    }

    public class QueryKeyResult
    {
        public string key { get; set; }
    }
}