using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class SyncronizeInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SyncronizationInformation Result { get; set; }
    }

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
            sb.AppendLine($"[{Height}] - TargetHeight: {TargetHeight} - Overview: {Overview}");
            sb.AppendLine("Peers:");
            sb.AppendLine(string.Join(Environment.NewLine, Peers));
            return sb.ToString();
        }
    }

    public class GeneralSyncronizationInformation
    {
        [JsonPropertyName("info")]
        public Connection Connection { get; set; }
        public override string ToString()
        {
            return $"{Connection}";
        }
    }

    public class Span
    {
        [JsonPropertyName("start_block_height")]
        public ulong StartBlockHeight { get; set; }
        [JsonPropertyName("nblocks")]
        public ulong N_Blocks { get; set; }
        [JsonPropertyName("connection_id")]
        public string ConnectionID { get; set; }
        [JsonPropertyName("rate")]
        public uint Rate { get; set; }
        [JsonPropertyName("speed")]
        public uint Speed { get; set; }
        [JsonPropertyName("size")]
        public ulong Size { get; set; }
        [JsonPropertyName("remote_address")]
        public string RemoteAddress { get; set; }
    }
}