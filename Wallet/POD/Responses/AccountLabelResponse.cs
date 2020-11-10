using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class AccountLabelResponse : RpcResponse
    {
        public AccountLabelResult result { get; set; }
    }

    public class AccountLabelResult
    {
        // ...
    }
}