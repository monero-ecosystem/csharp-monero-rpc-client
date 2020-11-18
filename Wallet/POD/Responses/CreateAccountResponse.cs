using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CreateAccountResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CreateAccount Result { get; set; }
    }

    public class CreateAccount
    {
        [JsonPropertyName("account_index")]
        public uint AccountIndex { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}