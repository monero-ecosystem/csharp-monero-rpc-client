using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class SubmitBlockResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitBlock Result { get; set; }
    }
}