using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class HardforkInformationResponse : RpcResponse
    {
        public HardforkInformationResult result { get; set; }
    }

    public class HardforkInformationResult
    {
        public uint credits { get; set; }
        public uint earliest_height { get; set; }
        public uint state { get; set; }
        public string status { get; set; }
        public uint threshold { get; set; }
        public string top_hash { get; set; }
        public bool untrusted { get; set; }
        public uint version { get; set; }
        public uint votes { get; set; }
        public uint voting { get; set; }
        public uint window { get; set; }
    }
}