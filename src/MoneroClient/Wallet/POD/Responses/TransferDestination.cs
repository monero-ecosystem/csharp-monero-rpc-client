using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    public class TransferDestination
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
    }
}