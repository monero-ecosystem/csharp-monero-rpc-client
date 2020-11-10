using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class FundTransferSplitResponse : RpcResponse
    {
        public FundTransferSplitResult result { get; set; }
    }

    public class FundTransferSplitResult
    {
        public List<string> tx_hash_list { get; set; } = new List<string>();
        public List<string> tx_key_list { get; set; } = new List<string>();
        public List<ulong> amount_list { get; set; } = new List<ulong>();
        public List<ulong> fee_list { get; set; } = new List<ulong>();
        public List<string> tx_metadata_list { get; set; } = new List<string>();
        public string multisig_txset { get; set; }
        public string unsigned_txset { get; set; }
        public List<uint> weight_list { get; set; }
    }
}