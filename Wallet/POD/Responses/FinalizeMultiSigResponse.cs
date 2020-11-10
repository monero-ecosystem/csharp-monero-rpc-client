using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
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