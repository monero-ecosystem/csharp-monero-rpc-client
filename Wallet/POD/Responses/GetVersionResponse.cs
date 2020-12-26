using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetRpcVersionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public GetVersion Result { get; set; }
    }

    public class GetVersion
    {
        [JsonPropertyName("version")]
        public uint Version { get; set; }
        public override string ToString()
        {
            return $"{Version}";
        }
    }
}