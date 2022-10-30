using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    public class ImportMultiSigInformation
    {
        [JsonPropertyName("n_outputs")]
        public uint N_Outputs { get; set; }
        public override string ToString()
        {
            return $"{this.N_Outputs}";
        }
    }
}