using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class BlockchainHeightResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockchainHeight Result { get; set; }
    }

    public class BlockchainHeight
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        public override string ToString()
        {
            return $"{Height}";
        }
    }
}