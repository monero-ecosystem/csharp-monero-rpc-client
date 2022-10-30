using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class IncomingTransfersResult
    {
        [JsonPropertyName("transfers")]
        public List<IncomingTransfer> Transfers { get; set; } = new List<IncomingTransfer>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.Transfers);
        }
    }
}