﻿using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    internal class GetVersionResult
    {
        [JsonPropertyName("version")]
        public uint Version { get; set; }
        public override string ToString()
        {
            return $"{this.Version}";
        }
    }
}