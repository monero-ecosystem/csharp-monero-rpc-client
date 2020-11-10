using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class GetRpcVersionResponse : RpcResponse
    {
        public GetVersionResult result { get; set; }
    }

    public class GetVersionResult
    {
        public uint version { get; set; }
    }
}