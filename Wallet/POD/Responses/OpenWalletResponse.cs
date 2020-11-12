using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class OpenWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public OpenWallet Result { get; set; }
    }

    public class OpenWallet
    {
        // ...
    }
}