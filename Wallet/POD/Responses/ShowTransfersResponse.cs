using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class ShowTransfersResponse : RpcResponse
    {
        public ShowTransfersResult result { get; set; }
    }

    public class ShowTransfersResult
    {
        public List<Transfer> @in { get; set; } = new List<Transfer>();
        public List<Transfer> @out { get; set; } = new List<Transfer>();
        public List<Transfer> pending { get; set; } = new List<Transfer>();
        public List<Transfer> failed { get; set; } = new List<Transfer>();
        public List<Transfer> pool { get; set; } = new List<Transfer>();
    }

    public class Transfer
    {
        public string address { get; set; }
        public ulong amount { get; set; }
        public List<ulong> amounts { get; set; } = new List<ulong>();
        public uint confirmations { get; set; }
        public ulong fee { get; set; }
        public bool double_spend_seen { get; set; }
        public uint height { get; set; }
        public bool locked { get; set; }
        public string note { get; set; }
        public string payment_id { get; set; }
        public SubaddressIndex subaddr_index { get; set; }
        public List<SubaddressIndex> subaddr_indices { get; set; } = new List<SubaddressIndex>();
        public uint suggested_confirmations_threshold { get; set; }
        public ulong timestamp { get; set; }
        public string txid { get; set; }
        public string type { get; set; }
        public ulong unlock_time { get; set; }
    }
}