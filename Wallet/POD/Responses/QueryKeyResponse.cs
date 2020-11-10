using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class QueryKeyResponse : RpcResponse
    {
        public QueryKeyResult result { get; set; }
    }

    public class QueryKeyResult
    {
        public string key { get; set; }
    }
}