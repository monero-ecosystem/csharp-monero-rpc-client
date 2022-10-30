using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignTransfer Result { get; set; }
    }
}