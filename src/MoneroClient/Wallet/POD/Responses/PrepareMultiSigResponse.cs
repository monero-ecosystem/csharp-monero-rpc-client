using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class PrepareMultiSigResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public PrepareMultiSigResult Result { get; set; }
    }
}