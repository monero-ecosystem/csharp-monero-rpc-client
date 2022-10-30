using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class MakeMultiSigResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public MakeMultiSig Result { get; set; }
    }
}