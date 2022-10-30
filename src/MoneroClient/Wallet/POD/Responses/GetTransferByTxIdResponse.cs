using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransferByTxidResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ShowTransferByTxidResult Result { get; set; }
    }
}