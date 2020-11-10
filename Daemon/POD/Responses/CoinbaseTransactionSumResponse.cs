using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
{
    public class CoinbaseTransactionSumResponse : RpcResponse
    {
        public CoinbaseTransactionSumResult result { get; set; }
    }

    public class CoinbaseTransactionSumResult
    {
        public uint credits { get; set; }
        public ulong emission_amount { get; set; }
        public ulong emission_amount_top64 { get; set; }
        public ulong fee_amount { get; set; }
        public ulong fee_amount_top64 { get; set; }
        public string status { get; set; }
        public string top_hash { get; set; }
        public bool untrusted { get; set; }
        public string wide_emision_amount { get; set; }
        public string wide_fee_amount { get; set; }
    }
}