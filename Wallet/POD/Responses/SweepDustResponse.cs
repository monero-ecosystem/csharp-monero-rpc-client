using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SweepDustResponse : RpcResponse
    {
        public SweepDustResult result { get; set; }
    }

    public class SweepDustResult
    {
        // ...
    }
}