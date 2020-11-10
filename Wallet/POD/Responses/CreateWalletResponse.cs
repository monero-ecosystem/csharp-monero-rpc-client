using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class CreateWalletResponse : RpcResponse
    {
        public CreateWalletResult result { get; set; }
    }

    public class CreateWalletResult
    {
        // ...
    }
}