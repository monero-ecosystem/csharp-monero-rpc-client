using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockHeaderResponse : RpcResponse
    {
        public BlockHeaderResult result { get; set; }
    }

    public class BlockHeaderResult
    {
        public BlockHeader block_header { get; set; }
        public string status { get; set; }
        public bool untrusted { get; set; }
        public string top_hash { get; set; }
        public uint credits { get; set; }
    }
}