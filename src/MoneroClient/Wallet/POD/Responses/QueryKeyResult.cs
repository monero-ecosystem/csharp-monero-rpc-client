using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class QueryKeyResult
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}