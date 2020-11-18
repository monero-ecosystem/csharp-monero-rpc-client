using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class Chain
    {
        [JsonPropertyName("block_hash")]
        public string BlockHash { get; set; }
        [JsonPropertyName("difficulty")]
        public ulong Difficulty { get; set; }
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("length")]
        public uint Length { get; set; }
    }
}