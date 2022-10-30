using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Addresses Result { get; set; }
    }
}