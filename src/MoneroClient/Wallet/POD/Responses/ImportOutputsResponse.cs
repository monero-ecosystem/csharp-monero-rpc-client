using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportOutputsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ImportOutputsResult Result { get; set; }
    }
}