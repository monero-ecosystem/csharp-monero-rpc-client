using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SweepSingleResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SweepSingle Result { get; set; }
    }
}