using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetAddressBookResponse : RpcResponse
    {
        public GetAddressBookResult result { get; set; }
    }

    public class GetAddressBookResult
    {
        public List<AddressBookEntry> entries { get; set; } = new List<AddressBookEntry>();
    }

    public class AddressBookEntry
    {
        public string address { get; set; }
        public string description { get; set; }
        public uint index { get; set; }
        public string payment_id { get; set; }
    }
}