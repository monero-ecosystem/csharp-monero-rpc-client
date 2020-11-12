using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class BlockchainHeightResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public BlockchainHeight Result { get; set; }
    }

    public class BlockchainHeight
    {
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
    }
}