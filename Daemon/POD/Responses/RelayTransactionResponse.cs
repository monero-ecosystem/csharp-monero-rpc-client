using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class RelayTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public RelayTransactionResult Result { get; set; }
    }

    public class RelayTransactionResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        public override string ToString()
        {
            return Status;
        }
    }
}