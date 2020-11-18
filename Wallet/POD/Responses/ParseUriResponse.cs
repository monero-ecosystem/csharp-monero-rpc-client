using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ParseUriResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ParseUri Result { get; set; }
    }

    public class ParseUri
    {
        [JsonPropertyName("uri")]
        public MoneroUri Uri { get; set; }
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
    }
}