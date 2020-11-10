using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class ConnectionResponse : RpcResponse
    {
        public ConnectionResult result { get; set; }
    }

    public class ConnectionResult
    {
        public List<Connection> connections { get; set; } = new List<Connection>();
        public string status { get; set; }
        public bool untrusted { get; set; }
    }
}