using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD.Requests
{
    internal class WalletRequest
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        public string method { get; set; }
        public WalletRequestParameters @params { get; set; }
    }
}