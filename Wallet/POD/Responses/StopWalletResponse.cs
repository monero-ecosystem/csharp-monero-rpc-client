using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class StopWalletResponse : RpcResponse
    {
        public StopWalletResult result { get; set; }
    }

    public class StopWalletResult
    {
        // ...
    }
}