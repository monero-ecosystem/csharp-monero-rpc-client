using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class Ban
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }
        [JsonPropertyName("ip")]
        public ulong IP { get; set; }
        [JsonPropertyName("seconds")]
        public uint Seconds { get; set; }
        public override string ToString()
        {
            return $"{this.Host} ({this.Seconds})";
        }
    }
}