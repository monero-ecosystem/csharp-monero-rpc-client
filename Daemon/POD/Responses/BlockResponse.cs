using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockResponse : RpcResponse
    {
        public Block result { get; set; }
    }
}