using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class BalanceResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Balance Result { get; set; }
    }
}