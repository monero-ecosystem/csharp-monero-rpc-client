using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class PaymentDetailResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public PaymentDetailResult Result { get; set; }
    }
}