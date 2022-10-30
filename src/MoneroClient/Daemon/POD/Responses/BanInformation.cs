using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{

    public class BanInformation
    {
        [JsonPropertyName("bans")]
        public List<Ban> Bans { get; set; } = new List<Ban>();
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        public override string ToString()
        {
            return string.Join(", ", this.Bans);
        }
    }
}