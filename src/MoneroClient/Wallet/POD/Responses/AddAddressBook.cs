using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    public class AddAddressBook
    {
        [JsonPropertyName("index")]
        public uint Index { get; set; }
        public override string ToString()
        {
            return $"Index {this.Index}";
        }
    }
}