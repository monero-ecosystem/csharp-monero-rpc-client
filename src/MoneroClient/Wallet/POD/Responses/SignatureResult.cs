using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignatureResult
    {
        [JsonPropertyName("signature")]
        public string Sig { get; set; }
        public override string ToString()
        {
            return this.Sig;
        }
    }
}