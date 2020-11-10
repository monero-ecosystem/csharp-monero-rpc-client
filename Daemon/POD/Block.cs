using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MoneroClient.Daemon.POD
{
    public class Block
    {
        public string blob { get; set; }
        public uint credits { get; set; }
        public BlockHeader block_header { get; set; }
        public string json { get; set; }
        public string miner_tx_hash { get; set; }
        public string status { get; set; }
        public string top_hash { get; set; }
        public bool untrusted { get; set; }
    }
}