using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class SignMultiSigTransactionResponse : RpcResponse
    {
        public SignMultiSigTransactionResult result { get; set; }
    }

    public class SignMultiSigTransactionResult
    {
        public string tx_data_hex { get; set; }
        public List<string> tx_hash_list { get; set; } = new List<string>();
    }
}