using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddAddressBookResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddAddressBook Result { get; set; }
    }

    public class AddAddressBook
    {
        [JsonPropertyName("index")]
        public uint Index { get; set; }
    }
}