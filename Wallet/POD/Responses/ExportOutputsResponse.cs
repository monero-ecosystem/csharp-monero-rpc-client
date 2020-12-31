using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ExportOutputsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ExportOutputsResult Result { get; set; }
    }

    internal class ExportOutputsResult
    {
        [JsonPropertyName("outputs_data_hex")]
        public string OutputsDataHex { get; set; }
        public override string ToString()
        {
            return OutputsDataHex;
        }
    }
}