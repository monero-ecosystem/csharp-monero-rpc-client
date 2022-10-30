using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MakeUriResult
    {
        [JsonPropertyName("uri")]
        public string Uri { get; set; }
        public override string ToString()
        {
            return this.Uri;
        }
    }
}