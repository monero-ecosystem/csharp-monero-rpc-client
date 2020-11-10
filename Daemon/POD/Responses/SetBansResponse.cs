using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
{
    public class SetBansResponse : RpcResponse
    {
        public SetBansResult result { get; set; }
    }

    public class SetBansResult
    {
        public string status { get; set; }
    }

}