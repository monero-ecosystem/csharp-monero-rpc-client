using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class GetBlockTemplateResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockTemplate Result { get; set; }
    }

    public class BlockTemplate
    {
        [JsonPropertyName("difficulty")]
        public ulong Difficulty { get; set; }
        [JsonPropertyName("wide_difficulty")]
        public string WideDifficulty { get; set; }
        [JsonPropertyName("difficulty_top64")]
        public ulong DifficultyTop64 { get; set; }
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("reserved_offset")]
        public ulong ReservedOffset { get; set; }
        [JsonPropertyName("expected_reward")]
        public ulong ExpectedReward { get; set; }
        [JsonPropertyName("prev_hash")]
        public string PreviousHash { get; set; }
        [JsonPropertyName("seed_height")]
        public ulong SeedHeight { get; set; }
        [JsonPropertyName("seed_hash")]
        public string SeedHash { get; set; }
        [JsonPropertyName("next_seed_hash")]
        public string NextSeedHash { get; set; }
        [JsonPropertyName("blocktemplate_blob")]
        public string BlockTemplateBlob { get; set; }
        [JsonPropertyName("blockhashing_blob")]
        public string BlockHashingBlob { get; set; }
        public override string ToString()
        {
            var typeInfo = typeof(BlockTemplate);
            var nonNullPropertyList = typeInfo.GetProperties()
                                              .Where(p => p.GetValue(this) != default)
                                              .Select(p => $"{p.Name}: {p.GetValue(this)} ");
            return string.Join(Environment.NewLine, nonNullPropertyList);
        }
    }
}