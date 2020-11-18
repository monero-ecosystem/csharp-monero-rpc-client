using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class DeleteAddressBookResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DeleteAddressBook Result { get; set; }
    }

    public class DeleteAddressBook
    {
        // ...
    }
}