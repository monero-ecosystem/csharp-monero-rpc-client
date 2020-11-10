using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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