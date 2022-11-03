using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    public class ValidateAddress
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }

        [JsonPropertyName("integrated")]
        public bool Integrated { get; set; }

        [JsonPropertyName("subaddress")]
        public bool Subaddress { get; set; }

        [JsonPropertyName("nettype")]
        public string NetType { get; set; }

        [JsonPropertyName("openalias_address")]
        public string OpenAliasAddress { get; set; }
    }
}
