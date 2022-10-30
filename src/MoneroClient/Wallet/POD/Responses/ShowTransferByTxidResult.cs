using Monero.Client.Network;
using System.Text.Json.Serialization;

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