using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

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