using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Utilities;

namespace Monero.Client.Daemon.POD
{
    public class Output
    {
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("target")]
        public Target Target { get; set; }
        public override string ToString()
        {
            return $"{PriceUtilities.PiconeroToMonero(this.Amount).ToString(PriceFormat.MoneroPrecision)} - {this.Target}";
        }
    }
}