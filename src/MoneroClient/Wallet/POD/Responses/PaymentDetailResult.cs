using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    internal class PaymentDetailResult
    {
        [JsonPropertyName("payments")]
        public List<PaymentDetail> Payments { get; set; } = new List<PaymentDetail>();
    }
}