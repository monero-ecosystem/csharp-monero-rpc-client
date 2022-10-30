using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    internal class BlockchainHeightResult
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        public override string ToString()
        {
            return $"{this.Height}";
        }
    }
}