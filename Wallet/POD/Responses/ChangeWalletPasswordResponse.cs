using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ChangeWalletPasswordResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ChangeWalletPassword Result { get; set; }
    }

    public class ChangeWalletPassword
    {
        // ...
    }
}