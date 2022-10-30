using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignMultiSigTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignMultiSigTransaction Result { get; set; }
    }
}