using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
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
            return $"Address: {this.Address} Amount: {PriceUtilities.PiconeroToMonero(this.Amount).ToString(PriceFormat.MoneroPrecision)} PaymentID: {this.PaymentID} RecipientName: {this.RecipientName} Description: {this.TransactionDescription}";
        }
    }
}