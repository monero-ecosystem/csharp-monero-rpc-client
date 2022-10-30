using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ShowTransferByTxidResult
    {
        [JsonPropertyName("transfer")]
        public Transfer Transfer { get; set; }
        public override string ToString()
        {
            return $"{this.Transfer}";
        }
    }
}