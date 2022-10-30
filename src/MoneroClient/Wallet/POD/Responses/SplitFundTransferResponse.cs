using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SplitFundTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SplitFundTransfer Result { get; set; }
    }
}