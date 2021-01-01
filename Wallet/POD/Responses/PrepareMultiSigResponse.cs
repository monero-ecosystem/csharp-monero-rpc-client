using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class PrepareMultiSigResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public PrepareMultiSigResult Result { get; set; }
    }

    internal class PrepareMultiSigResult
    {
        [JsonPropertyName("multisig_info")]
        public string MultiSigInformation { get; set; }
        public override string ToString()
        {
            return MultiSigInformation;
        }
    }
}