using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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