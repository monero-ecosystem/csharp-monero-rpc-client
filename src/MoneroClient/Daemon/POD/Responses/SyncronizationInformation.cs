using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class SyncronizationInformation
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("target_height")]
        public ulong TargetHeight { get; set; }
        [JsonPropertyName("credits")]
        public ulong Credits { get; set; }
        [JsonPropertyName("overview")]
        public string Overview { get; set; }
        [JsonPropertyName("peers")]
        public List<GeneralSyncronizationInformation> Peers { get; set; } = new List<GeneralSyncronizationInformation>();
        [JsonPropertyName("spans")]
        public List<Span> Spans { get; set; } = new List<Span>();
        [JsonPropertyName("untrusted")]
        public bool IsUntrusted { get; set; }
        [JsonPropertyName("next_needed_pruning_seed")]
        public uint NextNeededPruningSeed { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[{this.Height}] - TargetHeight: {this.TargetHeight} - Overview: {this.Overview}");
            sb.AppendLine("Peers:");
            sb.AppendLine(string.Join(Environment.NewLine, this.Peers));
            return sb.ToString();
        }
    }
}