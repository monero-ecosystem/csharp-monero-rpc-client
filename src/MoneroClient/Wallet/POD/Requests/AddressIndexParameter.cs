using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Requests
{
    internal class AddressIndexParameter
    {
        [JsonPropertyName("major")]
        public uint Major { get; set; }
        [JsonPropertyName("minor")]
        public uint Minor { get; set; }
    }
}