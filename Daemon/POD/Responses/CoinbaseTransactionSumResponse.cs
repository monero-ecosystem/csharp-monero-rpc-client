using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class CoinbaseTransactionSumResponse : RpcResponse
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