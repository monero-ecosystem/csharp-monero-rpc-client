using Monero.Client.Network;
using Monero.Client.Utilities;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class FundTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FundTransfer Result { get; set; }
    }
}