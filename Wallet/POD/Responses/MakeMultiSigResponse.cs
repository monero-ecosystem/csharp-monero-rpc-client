using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class MakeMultiSigResponse : RpcResponse
    {
        public MakeMultiSigResult result { get; set; }
    }

    public class MakeMultiSigResult
    {
        public string address { get; set; }
        public string multisig_info { get; set; }
    }
}