using Monero.Client.Network;
using Monero.Client.Utilities;
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
        public override string ToString()
        {
            return $"[{Height}] Unspent {PriceUtilities.PiconeroToMonero(Unspent):N12} / Spend {PriceUtilities.PiconeroToMonero(Spent):N12}";
        }
    }
}