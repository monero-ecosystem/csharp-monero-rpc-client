using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

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
    }
}