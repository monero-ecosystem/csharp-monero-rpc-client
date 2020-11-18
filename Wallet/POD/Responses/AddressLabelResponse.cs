using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressLabelResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddressLabel Result { get; set; }
    }

    public class AddressLabel
    {
        // ...
    }
}