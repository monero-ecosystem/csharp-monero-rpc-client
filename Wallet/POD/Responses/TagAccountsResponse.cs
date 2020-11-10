using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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