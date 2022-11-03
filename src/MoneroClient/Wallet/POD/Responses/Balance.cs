using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    public class Balance
    {
        /// <summary>
        /// The total balance of the current monero-wallet-rpc in session.
        /// </summary>
        [JsonPropertyName("balance")]
        public ulong TotalBalance { get; set; }

        /// <summary>
        /// Number of blocks before balance is safe to spend.
        /// </summary>
        [JsonPropertyName("blocks_to_unlock")]
        public ulong BlocksToUnlock { get; set; }
        [JsonPropertyName("multisig_import_needed")]
        public bool IsMultiSigImportNeeded { get; set; }
        [JsonPropertyName("per_subaddress")]
        public List<AddressDetails> SubaddressDetails { get; set; } = new List<AddressDetails>();

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

        public override string ToString()
        {
            return $"Unlocked {PriceUtilities.PiconeroToMonero(this.UnlockedBalance).ToString(PriceFormat.MoneroPrecision)} / Total {PriceUtilities.PiconeroToMonero(this.TotalBalance).ToString(PriceFormat.MoneroPrecision)}";
        }
    }
}