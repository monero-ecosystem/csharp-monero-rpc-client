using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountTagAndDescriptionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AccountTagAndDescription Result { get; set; }
    }

    public class AccountTagAndDescription
    {
        // ...
    }
}