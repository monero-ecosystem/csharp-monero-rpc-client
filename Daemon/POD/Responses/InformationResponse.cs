using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class NodeInformationResponse : RpcResponse
    {
        public InformationResult result { get; set; }

    }

    public class InformationResult
    {
        public ulong adjusted_time { get; set; }
        public uint alt_blocks_count { get; set; }
        public uint block_size_limit { get; set; }
        public uint block_size_median { get; set; }
        public uint block_weight_limit { get; set; }
        public uint block_weight_median { get; set; }
        public string bootstrap_daemon_address { get; set; }
        public uint credits { get; set; }
        public ulong cumulative_difficulty { get; set; }
        public ulong cumulative_difficulty_top64 { get; set; }
        public ulong database_size { get; set; }
        public ulong difficulty { get; set; }
        public ulong difficulty_top64 { get; set; }
        public ulong free_space { get; set; }
        public uint grey_peerlist_size { get; set; }
        public uint height { get; set; }
        public uint height_without_bootstrap { get; set; }
        public uint incoming_connections_count { get; set; }
        public bool mainnet { get; set; }
        public string nettype { get; set; }
        public bool offline { get; set; }
        public uint outgoing_connections_count { get; set; }
        public uint rpc_connections_count { get; set; }
        public bool stagenet { get; set; }
        public uint start_time { get; set; }
        public string status { get; set; }
        public uint target { get; set; }
        public uint target_height { get; set; }
        public bool testnet { get; set; }
        public string top_block_hash { get; set; }
        public string top_hash { get; set; }
        public uint tx_count { get; set; }
        public uint tx_pool_size { get; set; }
        public bool untrusted { get; set; }
        public bool update_available { get; set; }
        public string version { get; set; }
        public bool was_bootstrap_ever_used { get; set; }
        public uint white_peerlist_size { get; set; }
        public string wide_cumulative_difficulty { get; set; }
        public string wide_difficulty { get; set; }

    }
}