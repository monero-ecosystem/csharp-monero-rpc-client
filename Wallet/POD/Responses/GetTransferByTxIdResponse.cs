using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransferByTxidResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ShowTransferByTxid Result { get; set; }
    }

    public class ShowTransferByTxid
    {
        [JsonPropertyName("transfer")]
        public Transfer Transfer { get; set; }
    }
}