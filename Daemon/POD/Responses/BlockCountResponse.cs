using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
{
    public class BlockCountResponse : RpcResponse
    {
        public BlockCountResult result { get; set; }
    }

    public class BlockCountResult
    {
        public uint count { get; set; }
        public string status { get; set; }
        public bool untrusted { get; set; }
    }
}