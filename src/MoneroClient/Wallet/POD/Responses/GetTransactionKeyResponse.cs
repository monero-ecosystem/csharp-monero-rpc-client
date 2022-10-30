using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionKeyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetTransactionKeyResult Result { get; set; }
    }
}