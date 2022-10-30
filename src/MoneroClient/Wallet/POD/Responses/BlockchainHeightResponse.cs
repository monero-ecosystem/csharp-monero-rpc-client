using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class BlockchainHeightResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockchainHeightResult Result { get; set; }
    }
}