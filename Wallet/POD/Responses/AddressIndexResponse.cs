using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressIndexResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddressIndex Result { get; set; }
    }

    public class AddressIndex
    {
        [JsonPropertyName("major")]
        public uint Major { get; set; }
        [JsonPropertyName("minor")]
        public uint Minor { get; set; }
    }
}