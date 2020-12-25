using Monero.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class TransactionOutput
    {
        [JsonPropertyName("vout")]
        public List<Output> Vout { get; set; } = new List<Output>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Vout);
        }
    }

    public class Output
    {
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("target")]
        public Target Target { get; set; }
        public override string ToString()
        {
            return $"{PriceUtilities.PiconeroToMonero(Amount):N12} - {Target}";
        }
    }
    public class Target
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
        public override string ToString()
        {
            return Key;
        }
    }
}