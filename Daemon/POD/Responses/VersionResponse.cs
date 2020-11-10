using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
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