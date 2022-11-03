using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Daemon.POD.Responses
{
    public class CoinbaseTransactionSum
    {
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("emission_amount")]
        public ulong EmissionAmount { get; set; }
        [JsonPropertyName("emission_amount_top64")]
        public ulong EmissionAmountTop64 { get; set; }
        [JsonPropertyName("fee_amount")]
        public ulong FeeAmount { get; set; }
        [JsonPropertyName("fee_amount_top64")]
        public ulong FeeAmountTop64 { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("wide_emision_amount")]
        public string WideEmissionAmount { get; set; }
        [JsonPropertyName("wide_fee_amount")]
        public string WideFeeAmount { get; set; }
        public override string ToString()
        {
            return $"{this.TopHash} - Emission: {PriceUtilities.PiconeroToMonero(this.EmissionAmount).ToString(PriceFormat.MoneroPrecision)} - Fee: {PriceUtilities.PiconeroToMonero(this.FeeAmount).ToString(PriceFormat.MoneroPrecision)}";
        }
    }
}