using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class BlockHeader
    {
        [JsonPropertyName("block_size")]
        public ulong BlockSize { get; set; }
        [JsonPropertyName("block_weight")]
        public ulong BlockWeight { get; set; }
        [JsonPropertyName("cumulative_difficulty")]
        public ulong CumulativeDifficulty { get; set; }
        [JsonPropertyName("cumulative_difficulty_top64")]
        public ulong CumulativeDifficultyTop64 { get; set; }
        [JsonPropertyName("depth")]
        public ulong Depth { get; set; }
        [JsonPropertyName("difficulty")]
        public ulong Difficulty { get; set; }
        [JsonPropertyName("difficulty_top64")]
        public ulong DifficultyTop64 { get; set; }
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("long_term_weight")]
        public ulong LongTermWeight { get; set; }
        [JsonPropertyName("major_version")]
        public byte MajorVersion { get; set; }
        [JsonPropertyName("miner_tx_hash")]
        public string MinerTxHash { get; set; }
        [JsonPropertyName("minor_version")]
        public byte MinorVersion { get; set; }
        [JsonPropertyName("Nonce")]
        public uint Nonce { get; set; }
        [JsonPropertyName("num_txes")]
        public ulong NumTxes { get; set; }
        [JsonPropertyName("orphan_status")]
        public bool OrphanStatus { get; set; }
        [JsonPropertyName("pow_hash")]
        public string PowHash { get; set; }
        [JsonPropertyName("prev_hash")]
        public string PrevHash { get; set; }
        [JsonPropertyName("reward")]
        public ulong Reward { get; set; }
        [JsonPropertyName("timestamp")]
        public ulong Timestamp { get; set; }
        [JsonPropertyName("wide_cumulative_difficulty")]
        public string WideCumulativeDifficulty { get; set; }
        [JsonPropertyName("wide_difficulty")]
        public string WideDifficulty { get; set; }
        [JsonIgnore()]
        public DateTime DateTime 
        { 
            get
            {
                return new DateTime(1970, 1, 1).AddSeconds(this.Timestamp);
            }
        }
    }
}