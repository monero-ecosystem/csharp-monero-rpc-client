using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddressResult Result { get; set; }
    }

    public class AddressResult
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("addresses")]
        public List<AddressInformation> Addresses { get; set; } = new List<AddressInformation>();
    }

    public class AddressInformation
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("address_index")]
        public uint AddressIndex { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("used")]
        public bool IsUsed { get; set; }
    }
}