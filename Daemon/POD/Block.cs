using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class Block
    {
        [JsonPropertyName("blob")]
        public string Blob { get; set; }
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("block_header")]
        public BlockHeader BlockHeader { get; set; }
        [JsonPropertyName("json")]
        public string Json { get; set; }
        [JsonPropertyName("miner_tx_hash")]
        public string MinerTxHash { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, TopHash, BlockHeader);
        }
    }
}