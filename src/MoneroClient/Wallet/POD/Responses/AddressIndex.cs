using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class AddressIndex
    {
        [JsonPropertyName("major")]
        public uint Major { get; set; }
        [JsonPropertyName("minor")]
        public uint Minor { get; set; }
        public override string ToString()
        {
            return $"{this.Major} / {this.Minor}";
        }
    }
}