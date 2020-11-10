using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Network
{
    internal class RpcResponse
    {
        public string id { get; set; }
        public string jsonrpc { get; set; }
    }
}