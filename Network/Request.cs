using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Network
{
    internal class Request
    {
        public string jsonrpc { get; set; } = FieldAndHeaderDefaults.JsonRpc;
        public string id { get; set; } = FieldAndHeaderDefaults.Id;
    }
}