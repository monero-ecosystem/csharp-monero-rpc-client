using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD
{
    public class Recipient
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        public override string ToString()
        {
            return $"{this.Address} - {PriceUtilities.PiconeroToMonero(this.Amount).ToString(PriceFormat.MoneroPrecision)}";
        }
    }
}