using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Account Result { get; set; }
    }
}