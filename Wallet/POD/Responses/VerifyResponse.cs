using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class VerifyResponse : RpcResponse
    {
        public VerifyResult result { get; set; }
    }

    public class VerifyResult
    {
        public bool good { get; set; }
    }
}