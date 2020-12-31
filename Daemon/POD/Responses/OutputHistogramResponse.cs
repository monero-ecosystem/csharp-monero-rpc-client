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

    internal class OutputHistogramResult
    {
        [JsonPropertyName("distributions")]
        public List<Distribution> Distributions { get; set; } = new List<Distribution>();
        public override string ToString()
        {
            return string.Join(", ", Distributions);
        }
    }

    public class Distribution
    {
        [JsonPropertyName("data")]
        public OutputDistributionData Data { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("compressed_data")]
        public string CompressedData { get; set; }
        [JsonPropertyName("binary")]
        public bool IsBinary { get; set; }
        [JsonPropertyName("compress")]
        public bool IsCompressed { get; set; }

        public override string ToString()
        {
            return $"Amount: {PriceUtilities.PiconeroToMonero(Amount).ToString(PriceFormat.MoneroPrecision)} - IsCompressed: {IsCompressed} - CompressedData: {CompressedData} - IsBinary: {IsBinary}";
        }
    }

    public class OutputDistributionData
    {
        [JsonPropertyName("distribution")]
        public List<ulong> Distributions { get; set; } = new List<ulong>();
        [JsonPropertyName("start_height")]
        public ulong StartHeight { get; set; }
        [JsonPropertyName("base")]
        public ulong Base { get; set; }
    }
}