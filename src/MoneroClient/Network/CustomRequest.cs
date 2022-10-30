using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Network
{

    internal class CustomRequest : BaseRequest
    {
        /// <summary>
        /// Not all requests use the json_rpc interface. Those that don't need custom to pass arguments
        /// directly instead of via params.
        /// </summary>
        [JsonPropertyName("txs_hashes")]
        public IEnumerable<string> Txs_hashes { get; set; } = null;
    }
}