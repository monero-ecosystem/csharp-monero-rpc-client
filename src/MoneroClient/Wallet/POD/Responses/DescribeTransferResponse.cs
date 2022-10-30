using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class DescribeTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public DescribeTransferResult Result { get; set; }
    }
}