using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class DeleteAddressBookResponse : RpcResponse
    {
        public DeleteAddressBookResult result { get; set; }
    }

    public class DeleteAddressBookResult
    {
        // ...
    }
}