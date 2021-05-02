using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class TransactionDetails
    {
        [JsonPropertyName("version")]
        public uint Version { get; set; }
        [JsonPropertyName("unlock_time")]
        public ulong UnlockTime { get; set; }
    }
}