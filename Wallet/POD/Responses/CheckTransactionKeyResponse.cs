using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class CheckTransactionKeyResponse : RpcResponse
    {
        public CheckTransactionKeyResult result { get; set; }
    }

    public class CheckTransactionKeyResult
    {
        public uint confirmations { get; set; }
        public bool in_pool { get; set; }
        public ulong received { get; set; }

        [JsonIgnore()]
        public bool in_blockchain 
        { 
            get
            {
                return !in_pool;
            }
        }
    }
}