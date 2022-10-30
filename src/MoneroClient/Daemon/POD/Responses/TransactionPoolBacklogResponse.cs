using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class TransactionPoolBacklogResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public TransactionPoolBacklog Result { get; set; }
    }
}