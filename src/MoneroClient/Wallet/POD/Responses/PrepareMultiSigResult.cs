using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    internal class PrepareMultiSigResult
    {
        [JsonPropertyName("multisig_info")]
        public string MultiSigInformation { get; set; }
        public override string ToString()
        {
            return this.MultiSigInformation;
        }
    }
}