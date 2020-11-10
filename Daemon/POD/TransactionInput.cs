using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Daemon.POD
{
    public class TransactionInput
    {
        public List<Gen> gen { get; set; } = new List<Gen>();
    }

    public class Gen
    {
        public uint height { get; set; }
    }
}