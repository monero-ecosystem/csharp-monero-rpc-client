using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ValidateAddressResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ValidateAddress Result { get; set; }
    }
}