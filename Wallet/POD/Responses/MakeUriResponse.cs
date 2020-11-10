using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MakeUriResponse : RpcResponse
    {
        public MakeUriResult result { get; set; }
    }

    public class MakeUriResult
    {
        public string uri { get; set; }
    }
}