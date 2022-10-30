using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SweepSingleResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SweepSingle Result { get; set; }
    }
}