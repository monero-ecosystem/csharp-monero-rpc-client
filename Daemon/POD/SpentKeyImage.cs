using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class SpentKeyImage
    {
        [JsonPropertyName("id_hash")]
        public string KeyImage { get; set; }
        [JsonPropertyName("txs_hashes")]
        public List<string> TxHashes { get; set; } = new List<string>();
        public override string ToString()
        {
            return KeyImage;
        }
    }
}