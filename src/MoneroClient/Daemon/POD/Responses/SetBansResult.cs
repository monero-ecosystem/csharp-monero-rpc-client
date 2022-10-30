using Monero.Client.Network;
using System.Text.Json.Serialization;

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