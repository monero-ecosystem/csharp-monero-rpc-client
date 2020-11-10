using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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