using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CreateAccountResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CreateAccount Result { get; set; }
    }
}