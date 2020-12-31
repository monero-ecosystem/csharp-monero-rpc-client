using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class LanguagesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Languages Result { get; set; }
    }

    public class Languages
    {
        [JsonPropertyName("languages")]
        public List<string> AllLanguages { get; set; } = new List<string>();
        [JsonPropertyName("languages_local")]
        public List<string> LocalLanguages { get; set; } = new List<string>();
    }
}