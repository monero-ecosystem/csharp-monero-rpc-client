using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class ExportKeyImages
    {
        [JsonPropertyName("signed_key_images")]
        public List<SignedKeyImage> SignedKeyImages { get; set; } = new List<SignedKeyImage>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.SignedKeyImages);
        }
    }
}