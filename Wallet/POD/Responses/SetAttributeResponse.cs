using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SetAttributeResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetAttributeResult Result { get; set; }
    }

    internal class SetAttributeResult
    {
        // Empty
    }
}