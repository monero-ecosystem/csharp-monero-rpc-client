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
        [JsonPropertyName("tx_hash")]
        public string TxHash { get; set; }
        public override string ToString()
        {
            return TxHash;
        }
    }
}