using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitMultiSigTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitMultiSigResult Result { get; set; }
    }
}