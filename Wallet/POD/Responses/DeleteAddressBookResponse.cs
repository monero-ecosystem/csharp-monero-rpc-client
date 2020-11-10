using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class DeleteAddressBookResponse : RpcResponse
    {
        public DeleteAddressBookResult result { get; set; }
    }

    public class DeleteAddressBookResult
    {
        // ...
    }
}