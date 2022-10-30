using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class PruneBlockchainResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public PruneBlockchain Result { get; set; }
    }
}