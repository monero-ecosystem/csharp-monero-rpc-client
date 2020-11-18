using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ChangeWalletPasswordResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ChangeWalletPassword Result { get; set; }
    }

    public class ChangeWalletPassword
    {
        // ...
    }
}