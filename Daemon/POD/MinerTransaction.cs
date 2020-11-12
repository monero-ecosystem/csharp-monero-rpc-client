using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class MinerTransaction
    {
        [JsonPropertyName("version")]
        public uint Version { get; set; }
        [JsonPropertyName("unlock_time")]
        public ulong UnlockTime { get; set; }
        [JsonPropertyName("vin")]
        public List<TransactionInput> Vin { get; set; } = new List<TransactionInput>();
        [JsonPropertyName("vout")]
        public List<TransactionOutput> Vout { get; set; } = new List<TransactionOutput>();
        [JsonPropertyName("extra")]
        public List<uint> Extra { get; set; } = new List<uint>();
        [JsonPropertyName("signatures")]
        public List<string> Signatures { get; set; } = new List<string>();
        [JsonIgnore()]
        public TimeSpan EstimatedTimeTillUnlock
        {
            get
            {
                return BlockchainNetworkDefaults.AverageBlockTime * this.UnlockTime;
            }
        }
    }
}