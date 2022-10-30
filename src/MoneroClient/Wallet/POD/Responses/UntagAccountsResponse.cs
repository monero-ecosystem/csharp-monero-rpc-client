using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class UntagAccountsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public UntagAccounts Result { get; set; }
    }
}