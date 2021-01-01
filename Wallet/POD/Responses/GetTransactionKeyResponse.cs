using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionKeyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetTransactionKeyResult Result { get; set; }
    }

    internal class GetTransactionKeyResult
    {
        [JsonPropertyName("tx_key")]
        public string TransactionKey { get; set; }
        public override string ToString()
        {
            return TransactionKey;
        }
    }
}