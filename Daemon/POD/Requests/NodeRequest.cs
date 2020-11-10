using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Daemon.POD.Requests
{
    internal class DaemonRequest
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        public string method { get; set; }
        public DaemonRequestParameters @params { get; set; }
    }
}