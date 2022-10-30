using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SplitFundTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SplitFundTransfer Result { get; set; }
    }
}