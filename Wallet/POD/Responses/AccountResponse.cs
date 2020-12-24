using Monero.Client.Network;
using Monero.Client.Utilities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AccountResult Result { get; set; }
    }

    public class AccountResult
    {
        [JsonPropertyName("subaddress_accounts")]
        public List<SubaddressDetails> SubaddressAccounts { get; set; } = new List<SubaddressDetails>();
        [JsonPropertyName("total_balance")]
        public ulong TotalBalance { get; set; }
        [JsonPropertyName("total_unlocked_balance")]
        public ulong TotalUnlockedBalance { get; set; }
        public override string ToString()
        {
            return $"Unlocked {PriceUtilities.PiconeroToMonero(TotalUnlockedBalance)} / Balance {PriceUtilities.PiconeroToMonero(TotalBalance)}";
        }
    }
}