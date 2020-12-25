using System;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class Connection
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("address_type")]
        public byte AddressType { get; set; }
        [JsonPropertyName("avg_download")]
        public ulong AvgDownload { get; set; }
        [JsonPropertyName("avg_upload")]
        public ulong AvgUpload { get; set; }
        [JsonPropertyName("connection_id")]
        public string ConnectionID { get; set; }
        [JsonPropertyName("current_download")]
        public uint CurrentDownload { get; set; }
        [JsonPropertyName("current_upload")]
        public uint CurrentUpload { get; set; }
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("host")]
        public string Host { get; set; }
        [JsonPropertyName("live_time")]
        public ulong LiveTime { get; set; }
        [JsonPropertyName("local_ip")]
        public bool IsLocalID { get; set; }
        [JsonPropertyName("localhost")]
        public bool IsLocalhost { get; set; }
        [JsonPropertyName("peer_id")]
        public string PeerID { get; set; }
        [JsonPropertyName("port")]
        public string Port { get; set; }
        [JsonPropertyName("pruning_seed")]
        public uint PruningSeed { get; set; }
        [JsonPropertyName("recv_count")]
        public ulong RecvCount { get; set; }
        [JsonPropertyName("recv_idle_time")]
        public ulong RecvIdleTime { get; set; }
        [JsonPropertyName("recv_credits_per_hash")]
        public uint RecvCreditsPerHash { get; set; }
        [JsonPropertyName("rpc_port")]
        public UInt16 RpcPort { get; set; }
        [JsonPropertyName("send_count")]
        public ulong SendCount { get; set; }
        [JsonPropertyName("send_idle_time")]
        public ulong SendIdleTime { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("support_flags")]
        public uint SupportFlags { get; set; }
        [JsonIgnore()]
        public TimeSpan ConnectionTime
        {
            get
            {
                return TimeSpan.FromSeconds(this.LiveTime);
            }
        }
        public override string ToString()
        {
            return $"{Address} ({State})";
        }
    }
}