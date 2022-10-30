using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

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
        public override string ToString()
        {
            return $"[{this.AddressIndex}] ({this.Label}) - {(this.IsUsed ? "Used" : "Unused")} - {this.Address}";
        }
    }
}