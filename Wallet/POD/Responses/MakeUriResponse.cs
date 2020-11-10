using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class MakeUriResponse : RpcResponse
    {
        public MakeUriResult result { get; set; }
    }

    public class MakeUriResult
    {
        public string uri { get; set; }
    }
}