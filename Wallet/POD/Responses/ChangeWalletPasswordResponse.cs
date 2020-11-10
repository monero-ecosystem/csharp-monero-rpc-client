using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ChangeWalletPasswordResponse : RpcResponse
    {
        public ChangeWalletPasswordResult result { get; set; }
    }

    public class ChangeWalletPasswordResult
    {
        // ...
    }
}