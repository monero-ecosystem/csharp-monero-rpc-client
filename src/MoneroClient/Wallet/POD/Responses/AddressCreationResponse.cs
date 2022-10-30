using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressCreationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddressCreation Result { get; set; }
    }
}