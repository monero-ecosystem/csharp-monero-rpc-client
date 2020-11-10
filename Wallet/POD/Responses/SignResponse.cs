using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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