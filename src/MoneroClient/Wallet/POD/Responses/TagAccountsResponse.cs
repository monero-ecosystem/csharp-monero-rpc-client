using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class TagAccountsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public TagAccounts Result { get; set; }
    }
}