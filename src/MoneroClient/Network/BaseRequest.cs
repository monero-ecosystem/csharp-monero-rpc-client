using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Network
{
    internal class BaseRequest : Request
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("params")]
        public GenericRequestParameters Params { get; set; }
    }
}