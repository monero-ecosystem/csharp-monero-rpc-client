using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Network
{
    internal class BaseRequest : Request
    {
        public string method { get; set; }
        public GenericRequestParameters @params { get; set; }
    }

    internal class CustomRequest : BaseRequest
    {
        /// <summary>
        /// Not all requests use the json_rpc interface. Those that don't need custom to pass arguments
        /// directly instead of via params.
        /// </summary>
        public IEnumerable<string> txs_hashes { get; set; } = null;
    }
}