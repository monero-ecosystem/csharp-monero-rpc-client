using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountLabelResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AccountLabel Result { get; set; }
    }
}