using MoneroClient.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneroClient.Wallet.POD.Responses
{
    public class SubmitMultiSigTransactionResponse : RpcResponse
    {
        public SubmitMultiSigResult result { get; set; }
    }

    public class SubmitMultiSigResult
    {
        public List<string> tx_hash_list { get; set; } = new List<string>();
    }
}