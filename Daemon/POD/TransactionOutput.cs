using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Daemon.POD
{
    public class TransactionOutput
    {
        public List<Output> vout { get; set; } = new List<Output>();
    }

    public class Output
    {
        public ulong amount { get; set; }
        public Target target { get; set; }
    }
    public class Target
    {
        public string key { get; set; }
    }
}