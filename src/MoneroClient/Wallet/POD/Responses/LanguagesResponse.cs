using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class LanguagesResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Languages Result { get; set; }
    }
}