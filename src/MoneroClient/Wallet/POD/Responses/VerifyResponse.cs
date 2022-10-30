using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class VerifyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public VerifyResult Result { get; set; }
    }
}