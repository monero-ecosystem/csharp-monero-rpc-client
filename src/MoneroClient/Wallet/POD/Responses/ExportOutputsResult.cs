using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ExportOutputsResult
    {
        [JsonPropertyName("outputs_data_hex")]
        public string OutputsDataHex { get; set; }
        public override string ToString()
        {
            return this.OutputsDataHex;
        }
    }
}