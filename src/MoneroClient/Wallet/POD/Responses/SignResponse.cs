using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignatureResult Result { get; set; }
    }
}