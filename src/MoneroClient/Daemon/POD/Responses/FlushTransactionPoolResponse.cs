using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class FlushTransactionPoolResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FlushTransactionPoolResult Result { get; set; }
    }
}