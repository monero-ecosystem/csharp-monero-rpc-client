using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class FinalizeMultiSigResult
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        public override string ToString()
        {
            return this.Address;
        }
    }
}