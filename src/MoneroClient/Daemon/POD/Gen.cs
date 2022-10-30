using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class Gen
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        public override string ToString()
        {
            return $"{this.Height}";
        }
    }
}