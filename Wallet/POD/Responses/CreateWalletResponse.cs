using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CreateWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CreateWallet Result { get; set; }
    }

    public class CreateWallet
    {
        // ...
    }
}