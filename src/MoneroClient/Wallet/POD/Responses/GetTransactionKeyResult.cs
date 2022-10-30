using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionKeyResult
    {
        [JsonPropertyName("tx_key")]
        public string TransactionKey { get; set; }
        public override string ToString()
        {
            return this.TransactionKey;
        }
    }
}