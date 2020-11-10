using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
{
    public class VersionResponse : RpcResponse
    {
        public VersionResult result { get; set; }
    }

    public class VersionResult
    {
        public string status { get; set; }
        public bool untrusted { get; set; }
        public uint version { get; set; }
    }
}