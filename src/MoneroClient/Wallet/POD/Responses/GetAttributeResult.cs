using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetAttributeResult
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
        public override string ToString()
        {
            return this.Value;
        }
    }
}