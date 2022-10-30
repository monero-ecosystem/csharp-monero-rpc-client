using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Requests
{
    internal class FundTransferParameter
    {
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}