using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class RescanSpentResponse : RpcResponse
    {
        public RescanSpentResult result { get; set; }
    }

    public class RescanSpentResult
    {
        // ...
    }
}