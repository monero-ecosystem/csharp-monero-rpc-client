using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportKeyImagesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ImportKeyImages Result { get; set; }
    }
}