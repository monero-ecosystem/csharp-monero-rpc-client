using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class QueryKeyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public QueryKeyResult Result { get; set; }
    }
}