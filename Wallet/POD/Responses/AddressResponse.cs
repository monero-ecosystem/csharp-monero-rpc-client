using Monero.Client.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AddressResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public Addresses Result { get; set; }
    }

    public class Addresses
    {
        [JsonPropertyName("address")]
        public string PrimaryAddress { get; set; }
        [JsonPropertyName("addresses")]
        public List<AddressInformation> AllAddresses { get; set; } = new List<AddressInformation>();
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(PrimaryAddress);
            sb.AppendLine("All Addresses:");
            sb.AppendLine(string.Join(Environment.NewLine, AllAddresses));
            return sb.ToString();
        }
    }

    public class AddressInformation
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("address_index")]
        public uint AddressIndex { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("used")]
        public bool IsUsed { get; set; }
        public override string ToString()
        {
            return $"[{AddressIndex}] ({Label}) - {(IsUsed ? "Used" : "Unused")} - {Address}";
        }
    }
}