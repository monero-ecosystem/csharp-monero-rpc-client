using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportMultiSigInfoResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ImportMultiSigInformation Result { get; set; }
    }

    public class ImportMultiSigInformation
    {
        [JsonPropertyName("n_outputs")]
        public uint N_Outputs { get; set; }
        public override string ToString()
        {
            return $"{N_Outputs}";
        }
    }
}