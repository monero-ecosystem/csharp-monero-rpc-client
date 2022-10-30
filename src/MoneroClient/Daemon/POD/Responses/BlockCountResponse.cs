using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockCountResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockCountResult Result { get; set; }
    }
}