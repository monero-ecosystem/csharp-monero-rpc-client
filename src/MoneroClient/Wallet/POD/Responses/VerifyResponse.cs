using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class VerifyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public VerifyResult Result { get; set; }
    }
}