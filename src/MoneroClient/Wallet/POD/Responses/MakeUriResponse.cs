using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MakeUriResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public MakeUriResult Result { get; set; }
    }
}