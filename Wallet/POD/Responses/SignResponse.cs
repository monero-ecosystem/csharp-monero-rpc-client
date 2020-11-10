using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class SignResponse : RpcResponse
    {
        public SignResult result { get; set; }
    }

    public class SignResult
    {
        public string signature { get; set; }
    }
}