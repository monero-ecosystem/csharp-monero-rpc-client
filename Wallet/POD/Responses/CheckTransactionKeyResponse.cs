using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CheckTransactionKeyResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public CheckTransactionKey Result { get; set; }
    }

    public class CheckTransactionKey
    {
        [JsonPropertyName("confirmations")]
        public ulong Confirmations { get; set; }
        [JsonPropertyName("in_pool")]
        public bool IsInPool { get; set; }
        [JsonPropertyName("received")]
        public ulong Received { get; set; }

        [JsonIgnore()]
        public bool IsInBlockchain 
        { 
            get
            {
                return Confirmations > 0ul;
            }
        }
    }
}