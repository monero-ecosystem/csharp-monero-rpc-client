using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class SetBansResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        public override string ToString()
        {
            return this.Status;
        }
    }
}