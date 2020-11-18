using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetAddressBookResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetAddressBook Result { get; set; }
    }

    public class GetAddressBook
    {
        [JsonPropertyName("entries")]
        public List<AddressBookEntry> Entries { get; set; } = new List<AddressBookEntry>();
    }

    public class AddressBookEntry
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("index")]
        public ulong Index { get; set; }
        [JsonPropertyName("payment_id")]
        public string PaymentID { get; set; }
    }
}