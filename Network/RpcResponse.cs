using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Network
{
    public class RpcResponse
    {
        public string id { get; set; }
        public string jsonrpc { get; set; }
    }
}