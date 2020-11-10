using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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