using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class FinalizeMultiSigResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FinalizeMultiSigResult Result { get; set; }
    }
}