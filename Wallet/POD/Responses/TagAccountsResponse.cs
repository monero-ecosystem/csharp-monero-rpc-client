using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class TagAccountsResponse : RpcResponse
    {
        public TagAccountsResult result { get; set; }
    }

    public class TagAccountsResult
    {
        // ...
    }
}