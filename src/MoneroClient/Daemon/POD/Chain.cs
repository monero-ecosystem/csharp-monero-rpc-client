using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class Chain
    {
        [JsonPropertyName("block_hash")]
        public string BlockHash { get; set; }
        [JsonPropertyName("difficulty")]
        public ulong Difficulty { get; set; }
        [JsonPropertyName("wide_difficulty")]
        public string WideDifficulty { get; set; }
        [JsonPropertyName("difficulty_top64")]
        public ulong DifficultyTop64 { get; set; }
        [JsonPropertyName("block_hashes")]
        public List<string> BlockHashes { get; set; } = new List<string>();
        [JsonPropertyName("main_chain_parent_block")]
        public string MainChainParentBlock { get; set; }
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("length")]
        public uint Length { get; set; }
        public override string ToString()
        {
            return $"[{this.Height}] {this.BlockHash} - Difficulty: {this.Difficulty} - Length: {this.Length} - MainChainParentBlock: {this.MainChainParentBlock}";
        }
    }
}