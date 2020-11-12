using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class CoinbaseTransactionSumResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CoinbaseTransactionSumResult Result { get; set; }
    }

    public class CoinbaseTransactionSumResult
    {
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("emission_amount")]
        public ulong EmissionAmount { get; set; }
        [JsonPropertyName("emission_amount_top64")]
        public ulong EmissionAmountTop64 { get; set; }
        [JsonPropertyName("fee_amount")]
        public ulong FeeAmount { get; set; }
        [JsonPropertyName("fee_amount_top64")]
        public ulong FeeAmountTop64 { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("untrusted")]
        public bool Untrusted { get; set; }
        [JsonPropertyName("wide_emision_amount")]
        public string WideEmissionAmount { get; set; }
        [JsonPropertyName("wide_fee_amount")]
        public string WideFeeAmount { get; set; }
    }
}