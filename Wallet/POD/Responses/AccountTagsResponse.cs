using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountTagsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AccountTags Result { get; set; }
    }

    public class AccountTags
    {
        [JsonPropertyName("account_tags")]
        public List<AccountTag> AcccountTags { get; set; } = new List<AccountTag>();
    }

    public class AccountTag
    {
        [JsonPropertyName("accounts")]
        public List<uint> Accounts { get; set; } = new List<uint>();
        [JsonPropertyName("tag")]
        public string Tag { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        public override string ToString()
        {
            return $"({Tag}) {Label}";
        }
    }
}