using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class RefreshWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public RefreshWallet Result { get; set; }
    }
}