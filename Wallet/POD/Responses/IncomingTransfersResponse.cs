using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD.Responses
{
    public class IncomingTransfersResponse : RpcResponse
    {
        public IncomingTransferResult result { get; set; }
    }

    public class IncomingTransferResult
    {
        public List<IncomingTransfer> transfers { get; set; } = new List<IncomingTransfer>();
    }

    public class IncomingTransfer
    {
        public ulong amount { get; set; }
        public uint block_height { get; set; }
        public uint global_index { get; set; }
        public bool frozen { get; set; }
        public string key_image { get; set; }
        public bool spent { get; set; }
        public SubaddressIndex subaddr_index { get; set; }
        public string tx_hash { get; set; }
        public bool unlocked { get; set; }
    }
}