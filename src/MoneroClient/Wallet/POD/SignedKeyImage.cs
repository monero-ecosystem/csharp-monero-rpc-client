using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD
{
    public class SignedKeyImage : KeyImage
    {
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
        public override string ToString()
        {
            return this.Signature;
        }
    }
}