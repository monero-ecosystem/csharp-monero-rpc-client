using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Daemon.POD.Requests;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Requests;

namespace Monero.Client.Network
{
    /// <summary>
    /// Used for non json_rpc interface commands.
    /// </summary>
    internal class CustomRequestParameters : GenericRequestParameters
    {
        [JsonPropertyName("txs_hashes")]
        public IEnumerable<string> Txs_hashes { get; set; } = null;
    }
}