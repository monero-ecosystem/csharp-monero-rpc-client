using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CheckTransactionKeyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CheckTransactionKey Result { get; set; }
    }
}