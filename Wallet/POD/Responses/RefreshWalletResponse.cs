using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class RefreshWalletResponse : RpcResponse
    {
        public RefreshWalletResult result { get; set; }
    }

    public class RefreshWalletResult
    {
        public uint blocks_fetched { get; set; }
        public bool received_money { get; set; }
    }
}