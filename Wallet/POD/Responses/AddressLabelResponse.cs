using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressLabelResponse : RpcResponse
    {
        public AddressLabelResult result { get; set; }
    }

    public class AddressLabelResult
    {
        // ...
    }
}