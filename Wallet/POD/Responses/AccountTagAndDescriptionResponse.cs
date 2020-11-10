using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class AccountTagAndDescriptionResponse : RpcResponse
    {
        public AccountTagAndDescriptionResult result { get; set; }
    }

    public class AccountTagAndDescriptionResult
    {
        // ...
    }
}