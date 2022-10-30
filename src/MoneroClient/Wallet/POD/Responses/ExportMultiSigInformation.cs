using Monero.Client.Network;
using System.Text.Json.Serialization;

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