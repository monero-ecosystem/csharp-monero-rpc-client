using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class SetTransactionNotesResponse : RpcResponse
    {
        public SetTransactionNotesResult result { get; set; }
    }

    public class SetTransactionNotesResult
    {
        // ...
    }
}