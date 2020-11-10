using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
{ 
    public class BlockResponse : RpcResponse
    {
        public Block result { get; set; }
    }
}