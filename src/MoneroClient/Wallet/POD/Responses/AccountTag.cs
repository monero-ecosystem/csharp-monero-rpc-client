using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class AccountTag
    {
        [JsonPropertyName("accounts")]
        public List<uint> Accounts { get; set; } = new List<uint>();
        [JsonPropertyName("tag")]
        public string Tag { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        public override string ToString()
        {
            return $"({this.Tag}) {this.Label}";
        }
    }
}