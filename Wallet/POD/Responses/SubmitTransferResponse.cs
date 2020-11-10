using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class SubmitTransferResponse : RpcResponse
    {
        public SubmitTransferResult result { get; set; }
    }

    public class SubmitTransferResult
    {
        public List<string> tx_hash_list { get; set; } = new List<string>();
    }
}