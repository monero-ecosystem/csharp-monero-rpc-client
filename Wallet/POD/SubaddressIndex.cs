using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD
{
    public class SubaddressIndex
    {
        [JsonPropertyName("major")]
        public uint Major { get; set; }
        [JsonPropertyName("minor")]
        public uint Minor { get; set; }
    }
}