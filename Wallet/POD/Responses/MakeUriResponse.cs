using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MakeUriResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public MakeUriResult Result { get; set; }
    }

    internal class MakeUriResult
    {
        [JsonPropertyName("uri")]
        public string Uri { get; set; }
        public override string ToString()
        {
            return Uri;
        }
    }
}