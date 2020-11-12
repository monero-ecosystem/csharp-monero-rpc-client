using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountTagAndDescriptionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AccountTagAndDescription Result { get; set; }
    }

    public class AccountTagAndDescription
    {
        // ...
    }
}