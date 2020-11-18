using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class StopWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public StopWalletResult Result { get; set; }
    }

    public class StopWalletResult
    {
        // ...
    }
}