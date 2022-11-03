using System;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD
{
    public class AddressDetails
    {
        [JsonPropertyName("account_index")]
        public uint AccountIndex { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("address_index")]
        public uint AddressIndex { get; set; }
        [JsonPropertyName("balance")]
        public ulong Balance { get; set; }
        [JsonPropertyName("blocks_to_unlock")]
        public uint BlocksToUnlock { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("num_unspent_outputs")]
        public ulong NumUnspentOutputs { get; set; }

        /// <summary>
        /// Time (in seconds) before balance is safe to spend.
        /// </summary>
        [JsonPropertyName("time_to_unlock")]
        public ulong TimeToUnlock { get; set; }
        [JsonPropertyName("unlocked_balance")]
        public ulong UnlockedBalance { get; set; }
        [JsonIgnore]
        public TimeSpan EstimatedTimeTillUnlock
        {
            get
            {
                return BlockchainNetworkDefaults.AverageBlockTime * this.BlocksToUnlock;
            }
        }
    }
}