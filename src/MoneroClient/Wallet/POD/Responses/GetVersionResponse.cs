using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetRpcVersionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetVersionResult Result { get; set; }
    }
}