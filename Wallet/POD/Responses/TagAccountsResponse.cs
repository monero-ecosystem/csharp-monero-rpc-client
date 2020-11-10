using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class TagAccountsResponse : RpcResponse
    {
        public TagAccountsResult result { get; set; }
    }

    public class TagAccountsResult
    {
        // ...
    }
}