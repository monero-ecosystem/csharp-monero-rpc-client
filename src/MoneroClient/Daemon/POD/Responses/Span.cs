using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
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