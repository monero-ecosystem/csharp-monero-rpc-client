using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class PrepareMultiSigResponse : RpcResponse
    {
        public PrepareMultiSigResult result { get; set; }
    }

    public class PrepareMultiSigResult
    {
        public string multisig_info { get; set; }
    }
}