using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class PaymentDetailResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public PaymentDetailResult Result { get; set; }
    }

    internal class PaymentDetailResult
    {
        [JsonPropertyName("payments")]
        public List<PaymentDetail> Payments { get; set; } = new List<PaymentDetail>();
    }
}