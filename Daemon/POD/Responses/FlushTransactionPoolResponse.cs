using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class FlushTransactionPoolResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FlushTransactionPoolResult Result { get; set; }
    }

    public class FlushTransactionPoolResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}