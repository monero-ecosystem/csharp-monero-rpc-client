using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    public class AddressBook
    {
        [JsonPropertyName("entries")]
        public List<AddressBookEntry> Entries { get; set; } = new List<AddressBookEntry>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.Entries);
        }
    }
}