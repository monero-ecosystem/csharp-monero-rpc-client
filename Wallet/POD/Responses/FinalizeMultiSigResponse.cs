using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class FinalizeMultiSigResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FinalizeMultiSig Result { get; set; }
    }

    public class FinalizeMultiSig
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        public override string ToString()
        {
            return Address;
        }
    }
}