using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD
{
    public class PaymentDetail
    {
        [JsonPropertyName("payment_id")]
        public string PaymentID { get; set; }
        [JsonPropertyName("tx_hash")]
        public string TxHash { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("block_height")]
        public ulong Height { get; set; }
        [JsonPropertyName("unlock_time")]
        public ulong UnlockTime { get; set; }
        [JsonPropertyName("locked")]
        public bool IsLocked { get; set; }
        [JsonPropertyName("subaddr_index")]
        public SubaddressIndex SubaddressIndex { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        /// <summary>
        /// The amount of time it would take, from the moment the payment was made, until the payment would be unlocked.
        /// </summary>
        [JsonIgnore()]
        public TimeSpan EstimatedTimeTillUnlock
        {
            get
            {
                return BlockchainNetworkDefaults.AverageBlockTime * (BlockchainNetworkDefaults.BaseBlockUnlockThreshold + this.UnlockTime);
            }
        }
        public override string ToString()
        {
            var typeInfo = typeof(PaymentDetail);
            var nonNullPropertyList = typeInfo.GetProperties()
                                              .Where(p => p.GetValue(this) != default)
                                              .Select(p => $"{p.Name}: {p.GetValue(this)} ");
            return string.Join(Environment.NewLine, nonNullPropertyList);
        }
    }
}