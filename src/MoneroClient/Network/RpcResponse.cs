using System;
using System.Text.Json.Serialization;

namespace Monero.Client.Network
{
    internal class RpcResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("jsonrpc")]
        public string JsonRpc { get; set; }
        [JsonPropertyName("error")]
        public Error Error { get; set; }
        [JsonIgnore]
        public bool ContainsError
        {
            get
            {
                return this.Error != null && this.Error.Code != default;
            }
        }
    }
}