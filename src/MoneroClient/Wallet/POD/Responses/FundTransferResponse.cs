using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class FundTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FundTransfer Result { get; set; }
    }
}