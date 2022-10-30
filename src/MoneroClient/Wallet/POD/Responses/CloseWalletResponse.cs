using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CloseWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CloseWallet Result { get; set; }
    }
}