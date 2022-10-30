using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    internal class QueryKeyResult
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}