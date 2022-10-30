using Monero.Client.Network;
using System.Text.Json.Serialization;

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