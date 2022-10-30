﻿using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

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
        public override string ToString()
        {
            return $"[{this.Index}] {this.Address} - {this.Description} - {this.PaymentID}";
        }
    }
}