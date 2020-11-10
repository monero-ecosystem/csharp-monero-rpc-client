using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionNotesResponse : RpcResponse
    {
        public GetTransactionNotesResult result { get; set; }
    }

    public class GetTransactionNotesResult
    {
        public List<string> notes { get; set; } = new List<string>();
    }
}