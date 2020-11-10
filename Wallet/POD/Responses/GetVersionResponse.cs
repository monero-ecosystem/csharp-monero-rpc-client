using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
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