using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressCreationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddressCreation Result { get; set; }
    }

    public class AddressCreation
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("address_index")]
        public uint AddressIndex { get; set; }
        [JsonPropertyName("address_indices")]
        public List<uint> AddressIndices { get; set; } = new List<uint>();
        [JsonPropertyName("addresses")]
        public List<string> Addresses { get; set; } = new List<string>();
    }
}