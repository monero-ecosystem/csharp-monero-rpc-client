using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class BlockResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Block Result { get; set; }
    }
}