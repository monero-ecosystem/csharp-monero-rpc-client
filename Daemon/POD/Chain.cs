using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Daemon.POD
{
    public class Chain
    {
        public string block_hash { get; set; }
        public ulong difficulty { get; set; }
        public uint height { get; set; }
        public uint length { get; set; }
    }
}