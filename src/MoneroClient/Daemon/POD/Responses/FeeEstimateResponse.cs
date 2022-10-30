using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class FeeEstimateResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FeeEstimate Result { get; set; }
    }
}