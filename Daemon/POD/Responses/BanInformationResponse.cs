using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class BanInformationResponse : RpcResponse
    {
        public BanInformationResult result { get; set; }
    }

    public class BanInformationResult
    {
        public List<Ban> bans { get; set; } = new List<Ban>();
        public string status { get; set; }
        public bool untrusted { get; set; }
    }
    
    public class Ban
    {
        public string host { get; set; }
        public ulong ip { get; set; }
        public uint seconds { get; set; }
    }
}