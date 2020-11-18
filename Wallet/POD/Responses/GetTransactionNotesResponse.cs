using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionNotesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetTransactionNotes Result { get; set; }
    }

    public class GetTransactionNotes
    {
        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; } = new List<string>();
    }
}