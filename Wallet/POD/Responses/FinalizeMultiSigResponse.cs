using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class FinalizeMultiSigResponse : RpcResponse
    {
        public FinalizeMultiSigResult result { get; set; }
    }

    public class FinalizeMultiSigResult
    {
        public string address { get; set; }
    }
}