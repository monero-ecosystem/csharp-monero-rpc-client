using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class OpenWalletResponse : RpcResponse
    {
        public OpenWallet result { get; set; }
    }

    public class OpenWallet
    {
        // ...
    }
}