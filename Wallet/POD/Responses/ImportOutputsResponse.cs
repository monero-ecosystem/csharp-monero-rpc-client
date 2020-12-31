using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportOutputsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ImportOutputsResult Result { get; set; }
    }

    internal class ImportOutputsResult
    {
        [JsonPropertyName("num_imported")]
        public ulong NumImported { get; set; }
        public override string ToString()
        {
            return $"{NumImported} imported";
        }
    }
}