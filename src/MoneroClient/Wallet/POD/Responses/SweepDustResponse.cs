using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SweepDustResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SweepDust Result { get; set; }
    }
}