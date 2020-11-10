using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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