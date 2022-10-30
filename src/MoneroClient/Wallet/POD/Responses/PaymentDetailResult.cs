using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class PaymentDetailResult
    {
        [JsonPropertyName("payments")]
        public List<PaymentDetail> Payments { get; set; } = new List<PaymentDetail>();
    }
}