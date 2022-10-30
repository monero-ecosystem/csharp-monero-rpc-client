using Monero.Client.Network;
using Monero.Client.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class OutputHistogramResult
    {
        [JsonPropertyName("distributions")]
        public List<Distribution> Distributions { get; set; } = new List<Distribution>();
        public override string ToString()
        {
            return string.Join(", ", this.Distributions);
        }
    }
}