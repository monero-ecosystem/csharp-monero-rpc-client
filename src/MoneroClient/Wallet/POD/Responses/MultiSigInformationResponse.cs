using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MultiSigInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public MultiSigInformation Result { get; set; }
    }
}