using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class RelayTransactionResult
    {
        [JsonPropertyName("tx_hash")]
        public string TxHash { get; set; }
        public override string ToString()
        {
            return this.TxHash;
        }
    }
}