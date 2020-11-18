using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SetTransactionNotesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SetTransactionNotes Result { get; set; }
    }

    public class SetTransactionNotes
    {
        // ...
    }
}