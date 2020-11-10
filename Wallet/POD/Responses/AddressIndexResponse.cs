using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class AddressIndexResponse : RpcResponse
    {
        public AddressIndexResult result { get; set; }
    }

    public class AddressIndexResult
    {
        public uint major { get; set; }
        public uint minor { get; set; }
    }
}