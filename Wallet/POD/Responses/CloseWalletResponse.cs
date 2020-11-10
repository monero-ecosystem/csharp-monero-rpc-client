using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class CloseWalletResponse : RpcResponse
    {
        public CloseWalletResult result { get; set; }
    }

    public class CloseWalletResult
    {
        // ...
    }
}