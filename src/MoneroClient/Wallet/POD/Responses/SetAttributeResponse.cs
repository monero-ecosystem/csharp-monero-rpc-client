using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SetAttributeResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetAttributeResult Result { get; set; }
    }
}