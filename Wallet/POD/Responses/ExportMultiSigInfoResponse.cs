using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ExportMultiSigInfoResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ExportMultiSigInformation Result { get; set; }
    }

    public class ExportMultiSigInformation
    {
        [JsonPropertyName("info")]
        public string Information { get; set; }
    }
}