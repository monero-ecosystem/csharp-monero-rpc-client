using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ExportKeyImagesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ExportKeyImages Result { get; set; }
    }

    public class ExportKeyImages
    {
        [JsonPropertyName("signed_key_images")]
        public List<SignedKeyImage> SignedKeyImages { get; set; } = new List<SignedKeyImage>();
    }
}