using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddAddressBookResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddAddressBook Result { get; set; }
    }

    public class AddAddressBook
    {
        [JsonPropertyName("index")]
        public uint Index { get; set; }
    }
}