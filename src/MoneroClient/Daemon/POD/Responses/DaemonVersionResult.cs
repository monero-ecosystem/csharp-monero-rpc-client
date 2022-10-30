using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class DaemonVersionResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("version")]
        public uint Version { get; set; }
        public override string ToString()
        {
            return $"{this.Status} - {this.Version} - {(this.IsUntrusted ? "Untrusted" : "Trusted")}";
        }
    }
}