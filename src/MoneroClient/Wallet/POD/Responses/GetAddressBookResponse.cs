using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetAddressBookResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddressBook Result { get; set; }
    }
}