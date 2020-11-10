using System;
using System.Collections.Generic;
using System.Text;

namespace MoneroClient.Daemon.POD
{
    public class BlockDetails
    {
        public uint major_version { get; set; }
        public uint minor_version { get; set; }
        public ulong timestamp { get; set; }
        public string prev_id { get; set; }
        public uint nonce { get; set; }
        public MinerTransaction miner_tx { get; set; }
        public List<string> tx_hashes { get; set; } = new List<string>();
    }

}