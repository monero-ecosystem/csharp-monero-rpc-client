using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class AccountTags
    {
        [JsonPropertyName("account_tags")]
        public List<AccountTag> AcccountTags { get; set; } = new List<AccountTag>();
    }
}