using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class VersionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DaemonVersionResult Result { get; set; }
    }
}