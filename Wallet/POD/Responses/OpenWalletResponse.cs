using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD.Responses
{
    public class OpenWalletResponse : RpcResponse
    {
        public OpenWalletResult result { get; set; }
    }

    public class OpenWalletResult
    {
        // ...
    }
}