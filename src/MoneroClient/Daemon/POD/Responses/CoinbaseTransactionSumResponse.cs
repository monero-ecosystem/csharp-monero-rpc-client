using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class CoinbaseTransactionSumResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CoinbaseTransactionSum Result { get; set; }
    }
}