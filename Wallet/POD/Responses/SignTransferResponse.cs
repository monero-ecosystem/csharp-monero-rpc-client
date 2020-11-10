using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class SignTransferResponse : RpcResponse
    {
        public SignTransferResult result { get; set; }
    }

    public class SignTransferResult
    {
        public string signed_txset { get; set; }
        public List<string> tx_hash_list { get; set; } = new List<string>();
        public List<string> tx_raw_list { get; set; } = new List<string>();
    }
}