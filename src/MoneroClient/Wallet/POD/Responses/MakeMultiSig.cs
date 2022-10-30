using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    public class MakeMultiSig
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("multisig_info")]
        public string MultiSigInformation { get; set; }
        public override string ToString()
        {
            return $"{this.Address} - {this.MultiSigInformation}";
        }
    }
}