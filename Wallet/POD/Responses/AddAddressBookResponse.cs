using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class AddAddressBookResponse : RpcResponse
    {
        public AddAddressBookResult result { get; set; }
    }

    public class AddAddressBookResult
    {
        public uint index { get; set; }
    }
}