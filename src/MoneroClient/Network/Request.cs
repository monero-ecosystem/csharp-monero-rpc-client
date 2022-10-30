﻿using System.Text.Json.Serialization;

namespace Monero.Client.Network
{
    internal class Request
    {
        [JsonIgnore]
        public RequestEndpoint Endpoint { get; set; }
        [JsonPropertyName("jsonrpc")]
        public string Jsonrpc { get; set; } = FieldAndHeaderDefaults.JsonRpc;
        [JsonPropertyName("id")]
        public string Id { get; set; } = FieldAndHeaderDefaults.Id;
    }
}