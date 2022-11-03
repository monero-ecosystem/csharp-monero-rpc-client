using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Utilities;

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
        public override string ToString()
        {
            return $"[{this.AccountIndex}] ({this.Tag}) {this.BaseAddress} - Unlocked {PriceUtilities.PiconeroToMonero(this.UnlockedBalance).ToString(PriceFormat.MoneroPrecision)} / Total {PriceUtilities.PiconeroToMonero(this.Balance).ToString(PriceFormat.MoneroPrecision)}";
        }
    }
}