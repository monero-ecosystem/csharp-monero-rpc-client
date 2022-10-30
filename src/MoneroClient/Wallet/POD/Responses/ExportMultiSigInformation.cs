using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class ExportMultiSigInformation
    {
        [JsonPropertyName("info")]
        public string Information { get; set; }
        public override string ToString()
        {
            return this.Information;
        }
    }
}