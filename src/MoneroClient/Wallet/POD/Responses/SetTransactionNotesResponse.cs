using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SetTransactionNotesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SetTransactionNotes Result { get; set; }
    }
}