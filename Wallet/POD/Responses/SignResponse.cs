using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Signature Result { get; set; }
    }

    public class Signature
    {
        [JsonPropertyName("signature")]
        public string Sig { get; set; }
    }
}