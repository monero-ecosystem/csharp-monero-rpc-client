using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SendRawTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SendRawTransaction Result { get; set; }
    }

    public class SendRawTransaction
    {
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
        [JsonIgnore()]
        public bool IsRelayed 
        { 
            get
            {
                return !IsNotRelayed;
            }
        }
        [JsonPropertyName("not_relayed")]
        public bool IsNotRelayed { get; set; }
        [JsonPropertyName("low_mixin")]
        public bool IsLowMixin { get; set; }
        [JsonPropertyName("double_spend")]
        public bool IsDoubleSpend { get; set; }
        [JsonPropertyName("invalid_input")]
        public bool IsInvalidInput { get; set; }
        [JsonPropertyName("invalid_output")]
        public bool IsInvalidOutput { get; set; }
        [JsonPropertyName("too_big")]
        public bool IsTooBig { get; set; }
        [JsonPropertyName("overspend")]
        public bool IsOverspend { get; set; }
        [JsonPropertyName("fee_too_low")]
        public bool IsFeeTooLow { get; set; }
        [JsonPropertyName("too_few_outputs")]
        public bool IsTooFewOutputs { get; set; }
        [JsonPropertyName("sanity_check_failed")]
        public bool IsSanityCheckFailed { get; set; }
    }
}