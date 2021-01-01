using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransferByTxidResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ShowTransferByTxidResult Result { get; set; }
    }

    internal class ShowTransferByTxidResult
    {
        [JsonPropertyName("transfer")]
        public Transfer Transfer { get; set; }
        public override string ToString()
        {
            return $"{Transfer}";
        }
    }
}