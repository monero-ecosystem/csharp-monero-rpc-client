using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    public class IncomingTransfer
    {
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("block_height")]
        public ulong BlockHeight { get; set; }
        [JsonPropertyName("global_index")]
        public ulong GlobalIndex { get; set; }
        [JsonPropertyName("frozen")]
        public bool IsFrozen { get; set; }
        [JsonPropertyName("key_image")]
        public string KeyImage { get; set; }
        [JsonPropertyName("spent")]
        public bool IsSpent { get; set; }
        [JsonPropertyName("subaddr_index")]
        public SubaddressIndex SubaddressIndex { get; set; }
        [JsonPropertyName("tx_hash")]
        public string TransactionHash { get; set; }
        [JsonPropertyName("unlocked")]
        public bool IsUnlocked { get; set; }
        public override string ToString()
        {
            return $"[{this.BlockHeight}] - {this.TransactionHash} - {PriceUtilities.PiconeroToMonero(this.Amount).ToString(PriceFormat.MoneroPrecision)} - {(this.IsSpent ? "Spent" : "Unspent")} - {(this.IsUnlocked ? "Unlocked" : "Locked")}";
        }
    }
}