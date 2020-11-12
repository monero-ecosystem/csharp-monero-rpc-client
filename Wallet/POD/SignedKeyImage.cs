using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD
{
    public class SignedKeyImage : KeyImage
    {
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}