using System;
using System.Collections.Generic;
using System.Text;

namespace MoneroClient.Daemon.POD
{
    public class Connection
    {
        public string address { get; set; }
        public uint address_type { get; set; }
        public uint avg_download { get; set; }
        public uint avg_upload { get; set; }
        public string connection_id { get; set; }
        public uint current_download { get; set; }
        public uint current_upload { get; set; }
        public uint height { get; set; }
        public string host { get; set; }
        public uint live_time { get; set; }
        public bool local_ip { get; set; }
        public bool localhost { get; set; }
        public string peer_id { get; set; }
        public string port { get; set; }
        public uint pruning_seed { get; set; }
        public uint recv_count { get; set; }
        public uint recv_idle_time { get; set; }
        public uint recv_credits_per_hash { get; set; }
        public uint rpc_port { get; set; }
        public uint send_count { get; set; }
        public uint send_idle_time { get; set; }
        public string state { get; set; }
        public uint support_flags { get; set; }
    }
}