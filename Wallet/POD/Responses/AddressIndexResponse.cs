using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressIndexResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public AddressIndex Result { get; set; }
    }

    public class AddressIndex
    {
        [JsonPropertyName("major")]
        public uint Major { get; set; }
        [JsonPropertyName("minor")]
        public uint Minor { get; set; }
        public override string ToString()
        {
            return $"{Major} / {Minor}";
        }
    }
}