using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class GetTransactionNotesResult
    {
        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; } = new List<string>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.Notes);
        }
    }
}