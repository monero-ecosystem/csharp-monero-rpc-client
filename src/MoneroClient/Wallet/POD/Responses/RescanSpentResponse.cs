using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class RescanSpentResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public RescanSpent Result { get; set; }
    }
}