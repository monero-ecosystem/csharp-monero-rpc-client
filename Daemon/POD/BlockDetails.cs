using Monero.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
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
        [JsonIgnore()]
        public DateTime DateTime
        {
            get
            {
                return DateTime.UnixEpoch.AddSeconds(this.Timestamp);
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder($"({DateTime.ToString(DateFormat.DateTimeFormat)}) {MajorVersion} / {MinorVersion} {PrevID}");
            sb.AppendLine($"{MinerTx}");
            return sb.ToString();
        }
    }

}