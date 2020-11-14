using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Network
{
    internal class GenericRequest
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        public string method { get; set; }
        public GenericRequestParameters @params { get; set; }
    }
}