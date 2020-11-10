using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ShowTransferByTxidResponse : RpcResponse
    {
        public ShowTransferByTxidResult result { get; set; }
    }

    public class ShowTransferByTxidResult
    {
        public Transfer transfer { get; set; }
        public List<Transfer> transfers { get; set; } = new List<Transfer>();
    }
}