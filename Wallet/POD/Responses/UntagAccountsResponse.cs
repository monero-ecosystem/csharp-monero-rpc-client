using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class UntagAccountsResponse : RpcResponse
    {
        public UntagAccountsResult result { get; set; }
    }

    public class UntagAccountsResult
    {
        // ...
    }
}