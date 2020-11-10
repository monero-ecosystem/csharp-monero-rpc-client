using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockHeaderRangeResponse : RpcResponse
    {
        public BlockHeaderRangeResult result { get; set; }
    }

    public class BlockHeaderRangeResult
    {
        public uint credits { get; set; }
        public List<BlockHeader> headers { get; set; } = new List<BlockHeader>();
        public string status { get; set; }
        public bool untrusted { get; set; }
        public string top_hash { get; set; }
    }
}