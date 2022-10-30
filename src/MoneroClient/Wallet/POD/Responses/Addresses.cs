using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    public class Addresses
    {
        [JsonPropertyName("address")]
        public string PrimaryAddress { get; set; }
        [JsonPropertyName("addresses")]
        public List<AddressInformation> AllAddresses { get; set; } = new List<AddressInformation>();
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.PrimaryAddress);
            sb.AppendLine("All Addresses:");
            sb.AppendLine(string.Join(Environment.NewLine, this.AllAddresses));
            return sb.ToString();
        }
    }
}