using System.Text.Json.Serialization;

namespace Monero.Client.Network
{
    internal class AnonymousRequest : Request
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("params")]
        public dynamic Params { get; set; }
    }
}