using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class RefreshWalletResponse : RpcResponse
    {
        public RefreshWalletResult result { get; set; }
    }

    public class RefreshWalletResult
    {
        public uint blocks_fetched { get; set; }
        public bool received_money { get; set; }
    }
}