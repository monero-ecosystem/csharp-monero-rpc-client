using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ExportOutputsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ExportOutputsResult Result { get; set; }
    }
}