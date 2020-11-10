using System;
using System.Collections.Generic;
using System.Text;

namespace MoneroClient.Wallet.POD.Requests
{
    internal class AnonymousWalletRequest
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        public string method { get; set; }
        public dynamic @params { get; set; }
    }
}