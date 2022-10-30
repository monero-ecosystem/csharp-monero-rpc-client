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
            return string.Join(Environment.NewLine, this.Vout);
        }
    }
}