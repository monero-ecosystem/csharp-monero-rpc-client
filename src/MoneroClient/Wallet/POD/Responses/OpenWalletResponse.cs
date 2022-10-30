using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class OpenWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public OpenWallet Result { get; set; }
    }
}