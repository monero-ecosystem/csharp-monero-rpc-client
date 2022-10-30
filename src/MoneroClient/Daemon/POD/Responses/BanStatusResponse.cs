using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class GetBanStatusResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BanStatus Result { get; set; }
    }
}