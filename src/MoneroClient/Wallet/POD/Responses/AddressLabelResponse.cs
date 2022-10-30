using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressLabelResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddressLabel Result { get; set; }
    }
}