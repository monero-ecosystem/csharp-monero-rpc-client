using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SaveWalletResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SaveWallet Result { get; set; }
    }

    public class SaveWallet
    {
        // ...
    }
}