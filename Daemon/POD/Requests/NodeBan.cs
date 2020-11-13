using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Requests
{
    public class NodeBan
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }
        [JsonPropertyName("ip")]
        public ulong IP { get; set; }
        [JsonPropertyName("ban")]
        public bool IsBanned { get; set; }
        [JsonPropertyName("seconds")]
        public uint Seconds { get; set; }
    }
}