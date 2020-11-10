using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class IsMultiSigResponse : RpcResponse
    {
        public IsMultiSigResult result { get; set; }
    }

    public class IsMultiSigResult
    {
        public bool multisig { get; set; }
        public bool ready { get; set; }
        public uint threshold { get; set; }
        public uint total { get; set; }
    }
}