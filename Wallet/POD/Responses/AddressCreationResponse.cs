using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class AddressCreationResponse : RpcResponse
    {
        public AddressCreationResult result { get; set; }
    }

    public class AddressCreationResult
    {
        public string address { get; set; }
        public uint address_index { get; set; }
        public List<uint> address_indices { get; set; }
        public List<string> addresses { get; set; }
    }
}