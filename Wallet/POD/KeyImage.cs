using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD
{
    public class KeyImage
    {
        [JsonPropertyName("key_image")]
        public string Image { get; set; }
    }
}