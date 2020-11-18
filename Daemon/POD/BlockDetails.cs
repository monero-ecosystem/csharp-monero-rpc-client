using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class BlockDetails
    {
        [JsonPropertyName("major_version")]
        public byte MajorVersion { get; set; }
        [JsonPropertyName("minor_version")]
        public byte MinorVersion { get; set; }
        [JsonPropertyName("timestamp")]
        public ulong Timestamp { get; set; }
        [JsonPropertyName("prev_id")]
        public string PrevID { get; set; }
        [JsonPropertyName("nonce")]
        public uint Nonce { get; set; }
        [JsonPropertyName("miner_tx")]
        public MinerTransaction MinerTx { get; set; }
        [JsonPropertyName("tx_hashes")]
        public List<string> TxHashes { get; set; } = new List<string>();
    }

}