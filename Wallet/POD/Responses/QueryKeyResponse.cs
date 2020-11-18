using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class QueryKeyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public QueryKey Result { get; set; }
    }

    public class QueryKey
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}