using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionKeyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetTransactionKey Result { get; set; }
    }

    public class GetTransactionKey
    {
        [JsonPropertyName("tx_key")]
        public string TransactionKey { get; set; }
    }
}