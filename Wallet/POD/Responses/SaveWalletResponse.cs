using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class SaveWalletResponse : RpcResponse
    {
        public SaveWalletResult result { get; set; }
    }

    public class SaveWalletResult
    {
        // ...
    }
}