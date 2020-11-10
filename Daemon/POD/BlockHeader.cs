using System;
using System.Collections.Generic;
using System.Text;

namespace MoneroClient.Daemon.POD
{
    public class BlockHeader
    {
        public uint block_size { get; set; }
        public uint block_weight { get; set; }
        public ulong cumulative_difficulty { get; set; }
        public ulong cumulative_difficulty_top64 { get; set; }
        public uint depth { get; set; }
        public ulong difficulty { get; set; }
        public ulong difficulty_top64 { get; set; }
        public string hash { get; set; }
        public uint height { get; set; }
        public uint long_term_weight { get; set; }
        public uint major_version { get; set; }
        public string miner_tx_hash { get; set; }
        public uint minor_version { get; set; }
        public uint nonce { get; set; }
        public uint num_txes { get; set; }
        public bool orphan_status { get; set; }
        public string pow_hash { get; set; }
        public string prev_hash { get; set; }
        public ulong reward { get; set; }
        public ulong timestamp { get; set; }
        public string wide_cumulative_difficulty { get; set; }
        public string wide_difficulty { get; set; }
    }
}