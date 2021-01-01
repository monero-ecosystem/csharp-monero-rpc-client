using Monero.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD
{
    public class Recipient
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        public override string ToString()
        {
            return $"{Address} - {PriceUtilities.PiconeroToMonero(Amount).ToString(PriceFormat.MoneroPrecision)}";
        }
    }
}