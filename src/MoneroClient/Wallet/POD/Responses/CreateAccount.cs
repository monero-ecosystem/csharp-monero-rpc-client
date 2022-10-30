using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    public class CreateAccount
    {
        [JsonPropertyName("account_index")]
        public uint AccountIndex { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        public override string ToString()
        {
            return $"[{this.AccountIndex}] {this.Address}";
        }
    }
}