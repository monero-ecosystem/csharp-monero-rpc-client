using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    internal class ImportOutputsResult
    {
        [JsonPropertyName("num_imported")]
        public ulong NumImported { get; set; }
        public override string ToString()
        {
            return $"{this.NumImported} imported";
        }
    }
}