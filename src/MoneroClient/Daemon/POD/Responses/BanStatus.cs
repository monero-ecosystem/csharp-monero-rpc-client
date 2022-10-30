using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class BanStatus
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("banned")]
        public bool IsBanned { get; set; }
        [JsonPropertyName("seconds")]
        public uint Seconds { get; set; }
        public override string ToString()
        {
            return $"{(this.IsBanned ? $"Banned for {this.Seconds} seconds" : "Not banned")}";
        }
    }
}