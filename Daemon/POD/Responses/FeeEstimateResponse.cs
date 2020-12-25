using Monero.Client.Network;
using Monero.Client.Utilities;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class FeeEstimateResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FeeEstimate Result { get; set; }
    }

    public class FeeEstimate
    {
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("fee")]
        public ulong Fee { get; set; }
        [JsonPropertyName("quantization_mask")]
        public ulong QuantizationMask { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        public override string ToString()
        {
            return $"{PriceUtilities.PiconeroToMonero(Fee):N12}";
        }
    }
}