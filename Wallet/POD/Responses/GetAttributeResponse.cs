using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetAttributeResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetAttributeResult Result { get; set; }
    }

    internal class GetAttributeResult
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
        public override string ToString()
        {
            return Value;
        }
    }
}