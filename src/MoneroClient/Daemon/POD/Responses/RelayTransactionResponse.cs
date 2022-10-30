using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class RelayTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public RelayTransactionResult Result { get; set; }
    }
}