using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressResponse : RpcResponse
    {
        public AddressResult result { get; set; }
    }

    public class AddressResult
    {
        public string address { get; set; }
        public List<AddressInformation> addresses { get; set; } = new List<AddressInformation>();
    }

    public class AddressInformation
    {
        public string address { get; set; }
        public uint address_index { get; set; }
        public string label { get; set; }
        public bool used { get; set; }
    }
}