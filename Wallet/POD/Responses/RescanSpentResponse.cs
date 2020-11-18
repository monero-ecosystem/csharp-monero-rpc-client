using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class RescanSpentResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public RescanSpent Result { get; set; }
    }

    public class RescanSpent
    {
        // ...
    }
}