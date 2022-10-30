using System.Text.Json.Serialization;

namespace Monero.Client.Daemon.POD
{
    public class Target
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
        public override string ToString()
        {
            return this.Key;
        }
    }
}