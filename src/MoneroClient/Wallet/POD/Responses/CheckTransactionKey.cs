using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class CheckTransactionKey
    {
        [JsonPropertyName("confirmations")]
        public ulong Confirmations { get; set; }
        [JsonPropertyName("in_pool")]
        public bool IsInPool { get; set; }
        [JsonPropertyName("received")]
        public ulong Received { get; set; }

        [JsonIgnore]
        public bool IsInBlockchain
        {
            get
            {
                return this.Confirmations > 0ul;
            }
        }

        public override string ToString()
        {
            return $"Confirmations: {this.Confirmations}";
        }
    }
}