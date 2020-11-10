using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class FundTransferResponse : RpcResponse
    {
        public FundTransferResult result { get; set; }
    }

    public class FundTransferResult
    {
        public ulong amount { get; set; }
        public ulong fee { get; set; }
        public string multisig_txset { get; set; }
        public string tx_blob { get; set; }
        public string tx_hash { get; set; }
        public string tx_key { get; set; }
        public string tx_metadata { get; set; }
        public string unsigned_txset { get; set; }
        public uint weight { get; set; }
    }
}