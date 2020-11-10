using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class BlockchainHeightResponse : RpcResponse
    {
        public BlockchainHeightResult result { get; set; }
    }

    public class BlockchainHeightResult
    {
        public uint height { get; set; }
    }
}