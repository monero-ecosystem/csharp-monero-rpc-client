using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountLabelResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AccountLabel Result { get; set; }
    }

    public class AccountLabel
    {
        // ...
    }
}