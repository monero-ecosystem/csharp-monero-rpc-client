using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SweepDustResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SweepDust Result { get; set; }
    }
}