using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountTagsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AccountTags Result { get; set; }
    }
}