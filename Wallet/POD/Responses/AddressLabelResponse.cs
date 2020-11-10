using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class AddressLabelResponse : RpcResponse
    {
        public AddressLabelResult result { get; set; }
    }

    public class AddressLabelResult
    {
        // ...
    }
}