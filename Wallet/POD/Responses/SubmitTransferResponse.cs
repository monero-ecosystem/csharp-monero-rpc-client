using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitTransferResponse : RpcResponse
    {
        public SubmitTransferResult result { get; set; }
    }

    public class SubmitTransferResult
    {
        public List<string> tx_hash_list { get; set; } = new List<string>();
    }
}