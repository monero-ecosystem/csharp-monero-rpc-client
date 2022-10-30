using Monero.Client.Network;
using Monero.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    public class Balance
    {
        [JsonPropertyName("balance")]
        public ulong TotalBalance { get; set; }
        [JsonPropertyName("blocks_to_unlock")]
        public ulong BlocksToUnlock { get; set; }
        [JsonPropertyName("multisig_import_needed")]
        public bool IsMultiSigImportNeeded { get; set; }
        [JsonPropertyName("per_subaddress")]
        public List<AddressDetails> SubaddressDetails { get; set; } = new List<AddressDetails>();
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
        public override string ToString()
        {
            return $"Unlocked {PriceUtilities.PiconeroToMonero(this.UnlockedBalance).ToString(PriceFormat.MoneroPrecision)} / Total {PriceUtilities.PiconeroToMonero(this.TotalBalance).ToString(PriceFormat.MoneroPrecision)}";
        }
    }
}