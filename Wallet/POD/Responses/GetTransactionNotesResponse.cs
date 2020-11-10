using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class GetTransactionNotesResponse : RpcResponse
    {
        public GetTransactionNotesResult result { get; set; }
    }

    public class GetTransactionNotesResult
    {
        public List<string> notes { get; set; } = new List<string>();
    }
}