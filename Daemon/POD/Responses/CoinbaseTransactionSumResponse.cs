using Monero.Client.Network;
using Monero.Client.Utilities;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class CoinbaseTransactionSumResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CoinbaseTransactionSumResult Result { get; set; }
    }

    public class CoinbaseTransactionSumResult
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
            return $"{TopHash} - Emission: {PriceUtilities.PiconeroToMonero(EmissionAmount).ToString(PriceFormat.MoneroPrecision)} - Fee: {PriceUtilities.PiconeroToMonero(FeeAmount).ToString(PriceFormat.MoneroPrecision)}";
        }
    }
}