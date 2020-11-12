using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class BalanceResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Balance Result { get; set; }

    }

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
        [JsonIgnore()]
        public TimeSpan EstimatedTimeTillUnlock
        {
            get
            {
                return BlockchainNetworkDefaults.AverageBlockTime * this.BlocksToUnlock;
            }
        }
    }
}