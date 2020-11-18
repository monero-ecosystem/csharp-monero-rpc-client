using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class TransactionInput
    {
        [JsonPropertyName("gen")]
        public List<Gen> Gens { get; set; } = new List<Gen>();
    }

    public class Gen
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
    }
}