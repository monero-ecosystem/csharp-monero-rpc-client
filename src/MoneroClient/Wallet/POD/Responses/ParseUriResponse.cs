using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ParseUriResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ParseUriResult Result { get; set; }
    }
}