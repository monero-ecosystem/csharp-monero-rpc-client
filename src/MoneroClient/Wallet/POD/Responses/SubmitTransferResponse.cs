using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitTransferResult Result { get; set; }
    }
}