using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class GetBanStatusResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BanStatus Result { get; set; }
    }

    public class BanStatus
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("banned")]
        public bool IsBanned { get; set; }
        [JsonPropertyName("seconds")]
        public uint Seconds { get; set; }
        public override string ToString()
        {
            return $"{(IsBanned ? $"Banned for {Seconds} seconds" : "Not banned")}";
        }
    }
}