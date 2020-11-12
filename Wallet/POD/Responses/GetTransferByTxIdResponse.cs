using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransferByTxidResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ShowTransferByTxid Result { get; set; }
    }

    public class ShowTransferByTxid
    {
        [JsonPropertyName("transfer")]
        public Transfer Transfer { get; set; }
    }
}