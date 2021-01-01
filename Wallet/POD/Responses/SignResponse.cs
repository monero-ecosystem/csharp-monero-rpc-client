using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Signature Result { get; set; }
    }

    public class Signature
    {
        [JsonPropertyName("signature")]
        public string Sig { get; set; }
        public override string ToString()
        {
            return Sig;
        }
    }
}