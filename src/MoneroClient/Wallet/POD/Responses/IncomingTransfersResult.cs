using Monero.Client.Network;
using Monero.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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