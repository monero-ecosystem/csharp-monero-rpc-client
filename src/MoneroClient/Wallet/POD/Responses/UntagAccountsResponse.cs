using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class UntagAccountsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public UntagAccounts Result { get; set; }
    }
}