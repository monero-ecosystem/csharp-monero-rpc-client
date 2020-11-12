using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD
{
    public class SubaddressDetails
    {
        [JsonPropertyName("account_index")]
        public uint AccountIndex { get; set; }
        [JsonPropertyName("balance")]
        public ulong Balance { get; set; }
        [JsonPropertyName("base_address")]
        public string BaseAddress { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("tag")]
        public string Tag { get; set; }
        [JsonPropertyName("unlocked_balance")]
        public ulong UnlockedBalance { get; set; }
    }
}