using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class RefreshWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public RefreshWallet Result { get; set; }
    }

    public class RefreshWallet
    {
        [JsonPropertyName("blocks_fetched")]
        public ulong BlocksFetched { get; set; }
        [JsonPropertyName("received_money")]
        public bool ReceivedMoney { get; set; }
    }
}