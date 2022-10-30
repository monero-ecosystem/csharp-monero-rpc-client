using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitTransferResult Result { get; set; }
    }
}