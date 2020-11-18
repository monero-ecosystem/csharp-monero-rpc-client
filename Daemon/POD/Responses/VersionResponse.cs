using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class VersionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DaemonVersion Result { get; set; }
    }

    public class DaemonVersion
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("version")]
        public uint Version { get; set; }
    }
}