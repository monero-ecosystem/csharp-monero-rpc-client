using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class DescribeTransferResult
    {
        [JsonPropertyName("desc")]
        public List<TransferDescription> TransferDescriptions { get; set; } = new List<TransferDescription>();
    }
}