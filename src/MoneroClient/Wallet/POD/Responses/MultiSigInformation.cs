using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class MultiSigInformation
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
            return $"MultiSig? {this.IsMultiSig}, Ready? {this.IsReady}, Threshold: {this.Threshold}, Total: {this.Total}";
        }
    }
}