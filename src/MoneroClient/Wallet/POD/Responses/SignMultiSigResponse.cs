using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignMultiSigTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignMultiSigTransaction Result { get; set; }
    }
}