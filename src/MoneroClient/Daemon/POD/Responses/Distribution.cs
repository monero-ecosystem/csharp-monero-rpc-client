using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Daemon.POD.Responses
{
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
            return $"Amount: {PriceUtilities.PiconeroToMonero(this.Amount).ToString(PriceFormat.MoneroPrecision)} - IsCompressed: {this.IsCompressed} - CompressedData: {this.CompressedData} - IsBinary: {this.IsBinary}";
        }
    }
}