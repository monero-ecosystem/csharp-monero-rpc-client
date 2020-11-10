﻿using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ParseUriResponse : RpcResponse
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