using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SweepAllResponse : RpcResponse
    {
        // Result is formatted the same as FundTransferResponse.
        [JsonPropertyName("result")]
        public SplitFundTransfer Result { get; set; }
    }
}