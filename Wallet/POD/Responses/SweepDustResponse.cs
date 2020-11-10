using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class SweepDustResponse : RpcResponse
    {
        public SweepDustResult result { get; set; }
    }

    public class SweepDustResult
    {

    }
}