using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
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