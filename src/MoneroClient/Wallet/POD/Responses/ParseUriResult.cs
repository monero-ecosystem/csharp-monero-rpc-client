using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{

    internal class ParseUriResult
    {
        [JsonPropertyName("uri")]
        public MoneroUri Uri { get; set; }
        public override string ToString()
        {
            return $"{this.Uri}";
        }
    }
}