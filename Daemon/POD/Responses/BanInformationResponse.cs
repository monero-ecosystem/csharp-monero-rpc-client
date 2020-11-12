using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class GetBansResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BanInformation Result { get; set; }
    }

    public class BanInformation
    {
        [JsonPropertyName("bans")]
        public List<Ban> Bans { get; set; } = new List<Ban>();
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("untrusted")]
        public bool Untrusted { get; set; }
    }
    
    public class Ban
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }
        [JsonPropertyName("ip")]
        public ulong IP { get; set; }
        [JsonPropertyName("seconds")]
        public uint Seconds { get; set; }
    }
}