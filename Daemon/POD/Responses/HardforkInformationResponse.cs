using Monero.Client.Network;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class HardforkInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public HardforkInformation Result { get; set; }
    }

    public class HardforkInformation
    {
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("earliest_height")]
        public ulong EarliestHeight { get; set; }
        [JsonPropertyName("state")]
        public uint State { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("threshold")]
        public byte Threshold { get; set; }
        [JsonPropertyName("top_hash")]
        public string TopHash { get; set; }
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("version")]
        public byte Version { get; set; }
        [JsonPropertyName("votes")]
        public uint Votes { get; set; }
        [JsonPropertyName("voting")]
        public byte Voting { get; set; }
        [JsonPropertyName("window")]
        public uint Window { get; set; }
        public override string ToString()
        {
            var typeInfo = typeof(HardforkInformation);
            var nonNullPropertyList = typeInfo.GetProperties()
                                              .Where(p => p.GetValue(this) != default)
                                              .Select(p => $"{p.Name}: {p.GetValue(this)}");
            return string.Join(Environment.NewLine, nonNullPropertyList);
        }
    }
}