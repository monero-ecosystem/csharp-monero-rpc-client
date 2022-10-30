using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportMultiSigInfoResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ImportMultiSigInformation Result { get; set; }
    }
}