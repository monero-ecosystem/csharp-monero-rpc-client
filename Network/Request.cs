using System.Text.Json.Serialization;

namespace Monero.Client.Network
{
    internal class Request
    {
        [JsonIgnore()]
        public RequestEndpoint endpoint { get; set; }
        public string jsonrpc { get; set; } = FieldAndHeaderDefaults.JsonRpc;
        public string id { get; set; } = FieldAndHeaderDefaults.Id;
    }
}