using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class SetBansResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SetBansResult Result { get; set; }
    }
}