using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    internal class VerifyResult
    {
        [JsonPropertyName("good")]
        public bool IsGood { get; set; }
        public override string ToString()
        {
            return $"{(this.IsGood ? "Good" : "Bad")}";
        }
    }
}