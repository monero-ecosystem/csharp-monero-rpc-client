using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Daemon.POD.Responses
{
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