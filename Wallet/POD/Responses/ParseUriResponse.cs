﻿using Monero.Client.Network;
using Monero.Client.Utilities;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ParseUriResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ParseUriResult Result { get; set; }
    }

    internal class ParseUriResult
    {
        [JsonPropertyName("uri")]
        public MoneroUri Uri { get; set; }
        public override string ToString()
        {
            return $"{Uri}";
        }
    }

    public class MoneroUri
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("payment_id")]
        public string PaymentID { get; set; }
        [JsonPropertyName("recipient_name")]
        public string RecipientName { get; set; }
        [JsonPropertyName("tx_description")]
        public string TransactionDescription { get; set; }
        public override string ToString()
        {
            return $"Address: {Address} Amount: {PriceUtilities.PiconeroToMonero(Amount).ToString(PriceFormat.MoneroPrecision)} PaymentID: {PaymentID} RecipientName: {RecipientName} Description: {TransactionDescription}";
        }
    }
}