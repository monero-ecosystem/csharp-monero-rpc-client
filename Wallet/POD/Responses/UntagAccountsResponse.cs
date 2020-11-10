using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class UntagAccountsResponse : RpcResponse
    {
        public UntagAccountsResult result { get; set; }
    }

    public class UntagAccountsResult
    {
        // ...
    }
}