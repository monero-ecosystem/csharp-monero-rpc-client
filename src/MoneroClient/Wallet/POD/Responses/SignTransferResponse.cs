using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignTransfer Result { get; set; }
    }
}