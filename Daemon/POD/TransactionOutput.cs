using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class TransactionOutput
    {
        [JsonPropertyName("vout")]
        public List<Output> Vout { get; set; } = new List<Output>();
    }

    public class Output
    {
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("target")]
        public Target Target { get; set; }
    }
    public class Target
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}