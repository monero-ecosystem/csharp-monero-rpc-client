using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockHeaderResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockHeaderResult Result { get; set; }
    }
}