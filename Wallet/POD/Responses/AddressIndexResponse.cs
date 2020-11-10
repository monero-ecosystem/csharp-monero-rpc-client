using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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