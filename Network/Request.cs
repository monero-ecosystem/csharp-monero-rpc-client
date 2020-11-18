namespace Monero.Client.Network
{
    internal class Request
    {
        public string jsonrpc { get; set; } = FieldAndHeaderDefaults.JsonRpc;
        public string id { get; set; } = FieldAndHeaderDefaults.Id;
    }
}