using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class PrepareMultiSigResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public PrepareMultiSig Result { get; set; }
    }

    public class PrepareMultiSig
    {
        [JsonPropertyName("multisig_info")]
        public string MultiSigInformation { get; set; }
    }
}