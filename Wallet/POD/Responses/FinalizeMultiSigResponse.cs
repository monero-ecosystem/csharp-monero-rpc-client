using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class FinalizeMultiSigResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public FinalizeMultiSig Result { get; set; }
    }

    public class FinalizeMultiSig
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}