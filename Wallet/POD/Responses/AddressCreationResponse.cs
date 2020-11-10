using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressCreationResponse : RpcResponse
    {
        public AddressCreationResult result { get; set; }
    }

    public class AddressCreationResult
    {
        public string address { get; set; }
        public uint address_index { get; set; }
        public List<uint> address_indices { get; set; } = new List<uint>();
        public List<string> addresses { get; set; } = new List<string>();
    }
}