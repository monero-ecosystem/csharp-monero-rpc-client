using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportKeyImagesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ImportKeyImages Result { get; set; }
    }

    public class ImportKeyImages
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("spent")]
        public ulong Spent { get; set; }
        [JsonPropertyName("unspent")]
        public ulong Unspent { get; set; }
    }
}