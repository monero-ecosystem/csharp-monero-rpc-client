using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class VerifyResponse : RpcResponse
    {
        public VerifyResult result { get; set; }
    }

    public class VerifyResult
    {
        public bool good { get; set; }
    }
}