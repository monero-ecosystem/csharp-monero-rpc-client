using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class DeleteAddressBookResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DeleteAddressBook Result { get; set; }
    }
}