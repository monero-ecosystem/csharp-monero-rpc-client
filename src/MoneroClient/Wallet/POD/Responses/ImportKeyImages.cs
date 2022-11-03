using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    public class ImportKeyImages
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("spent")]
        public ulong Spent { get; set; }
        [JsonPropertyName("unspent")]
        public ulong Unspent { get; set; }
        public override string ToString()
        {
            return $"[{this.Height}] Unspent {PriceUtilities.PiconeroToMonero(this.Unspent).ToString(PriceFormat.MoneroPrecision)} / Spend {PriceUtilities.PiconeroToMonero(this.Spent).ToString(PriceFormat.MoneroPrecision)}";
        }
    }
}