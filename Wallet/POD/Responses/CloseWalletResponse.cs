using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CloseWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CloseWallet Result { get; set; }
    }

    public class CloseWallet
    {
        // ...
    }
}