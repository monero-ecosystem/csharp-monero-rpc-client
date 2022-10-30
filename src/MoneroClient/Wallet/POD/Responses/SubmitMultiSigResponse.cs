using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitMultiSigTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitMultiSigResult Result { get; set; }
    }
}