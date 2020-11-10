using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class BlockchainHeightResponse : RpcResponse
    {
        public BlockchainHeightResult result { get; set; }
    }

    public class BlockchainHeightResult
    {
        public uint height { get; set; }
    }
}