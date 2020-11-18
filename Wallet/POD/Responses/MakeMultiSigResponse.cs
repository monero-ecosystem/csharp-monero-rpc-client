using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MakeMultiSigResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public MakeMultiSig Result { get; set; }
    }

    public class MakeMultiSig
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("multisig_info")]
        public string MultiSigInformation { get; set; }
    }
}