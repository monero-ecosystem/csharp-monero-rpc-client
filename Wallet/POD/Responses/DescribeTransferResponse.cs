using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class DescribeTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DescribeTransferResult Result { get; set; }
    }

    internal class DescribeTransferResult
    {
        [JsonPropertyName("desc")]
        public List<TransferDescription> TransferDescriptions { get; set; } = new List<TransferDescription>();
    }

    public class TransferDescription
    {
        [JsonPropertyName("amount_in")]
        public ulong AmountIn { get; set; }
        [JsonPropertyName("amount_out")]
        public ulong AmountOut { get; set; }
        [JsonPropertyName("ring_size")]
        public uint RingSize { get; set; }
        [JsonPropertyName("unlock_time")]
        public ulong UnlockTime { get; set; }
        [JsonPropertyName("recipients")]
        public List<Recipient> Recipients { get; set; } = new List<Recipient>();
        [JsonPropertyName("payment_id")]
        public string PaymentID { get; set; }
        [JsonPropertyName("change_amount")]
        public ulong ChangeAmount { get; set; }
        [JsonPropertyName("change_address")]
        public string ChangeAddress { get; set; }
        [JsonPropertyName("fee")]
        public ulong Fee { get; set; }
        [JsonPropertyName("dummy_outputs")]
        public uint DummyOutputs { get; set; }
        [JsonPropertyName("extra")]
        public string Extra { get; set; }
        public override string ToString()
        {
            var typeInfo = typeof(TransferDescription);
            var nonNullPropertyList = typeInfo.GetProperties()
                                              .Where(p => p.GetValue(this) != default)
                                              .Select(p => $"{p.Name}: {p.GetValue(this)} ");
            return string.Join(Environment.NewLine, nonNullPropertyList);
        }
    }


}