using Monero.Client.Network;
using Monero.Client.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class OutputHistogramResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public OutputHistogramResult Result { get; set; }
    }

    public class OutputHistogramResult
    {
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("histogram")]
        public List<Histogram> Histograms { get; set; } = new List<Histogram>();
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        public override string ToString()
        {
            return string.Join(", ", Histograms);
        }
    }

    public class Histogram
    {
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("recent_instances")]
        public ulong RecentInstances { get; set; }
        [JsonPropertyName("total_instances")]
        public ulong TotalInstances { get; set; }
        [JsonPropertyName("unlocked_instances")]
        public ulong UnlockedInstances { get; set; }
        public override string ToString()
        {
            return $"Amount: {PriceUtilities.PiconeroToMonero(Amount).ToString(PriceFormat.MoneroPrecision)} RecentInstances: {RecentInstances} UnlockedInstances: {UnlockedInstances} TotalInstances: {TotalInstances}";
        }
    }
}