using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Daemon.POD
{
    public class MinerTransaction
    {
        public uint version { get; set; }
        public uint unlock_time { get; set; }
        public List<TransactionInput> vin { get; set; } = new List<TransactionInput>();
        public List<TransactionOutput> vout { get; set; } = new List<TransactionOutput>();
        public List<uint> extra { get; set; } = new List<uint>();
        public List<string> signatures { get; set; } = new List<string>();
    }
}