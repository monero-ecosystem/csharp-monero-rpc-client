using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ShowTransferByTxidResponse : RpcResponse
    {
        public ShowTransferByTxidResult result { get; set; }
    }

    public class ShowTransferByTxidResult
    {
        public Transfer transfer { get; set; }
        public List<Transfer> transfers { get; set; } = new List<Transfer>();
    }
}