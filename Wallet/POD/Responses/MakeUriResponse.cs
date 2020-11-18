using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MakeUriResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public MakeUri Result { get; set; }
    }

    public class MakeUri
    {
        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }
}