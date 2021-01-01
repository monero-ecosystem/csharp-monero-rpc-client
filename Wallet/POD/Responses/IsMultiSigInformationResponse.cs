using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class IsMultiSigInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public IsMultiSigInformation Result { get; set; }
    }

    public class IsMultiSigInformation
    {
        [JsonPropertyName("multisig")]
        public bool IsMultiSig { get; set; }
        [JsonPropertyName("ready")]
        public bool IsReady { get; set; }
        [JsonPropertyName("threshold")]
        public uint Threshold { get; set; }
        [JsonPropertyName("total")]
        public uint Total { get; set; }
        public override string ToString()
        {
            return $"MultiSig? {IsMultiSig}, Ready? {IsReady}, Threshold: {Threshold}, Total: {Total}";
        }
    }
}