using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ParseUriResponse : RpcResponse
    {
        public ParseUriResult result { get; set; }
    }

    public class ParseUriResult
    {
        public MoneroUri uri { get; set; }
    }

    public class MoneroUri
    {
        public string address { get; set; }
        public ulong amount { get; set; }
        public string payment_id { get; set; }
        public string recipient_name { get; set; }
        public string tx_description { get; set; }
    }
}