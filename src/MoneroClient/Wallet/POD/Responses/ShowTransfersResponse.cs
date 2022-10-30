using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ShowTransfersResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ShowTransfers Result { get; set; }
    }
}