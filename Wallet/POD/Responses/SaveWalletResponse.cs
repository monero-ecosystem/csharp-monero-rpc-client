using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SaveWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SaveWallet Result { get; set; }
    }

    public class SaveWallet
    {
        // ...
    }
}