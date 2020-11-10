using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountLabelResponse : RpcResponse
    {
        public AccountLabelResult result { get; set; }
    }

    public class AccountLabelResult
    {
        // ...
    }
}