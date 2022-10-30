using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CreateWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CreateWallet Result { get; set; }
    }
}