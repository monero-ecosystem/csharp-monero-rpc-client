using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class IsMultiSigInformationResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public IsMultiSigInformation Result { get; set; }
    }

    public class IsMultiSigInformation
    {
        [JsonPropertyName("multisig")]
        public bool IsMultiSig { get; set; }
        [JsonPropertyName("ready")]
        public bool IsReady { get; set; }
        [JsonPropertyName("threshold")]
        public uint Threshold { get; set; }
        [JsonPropertyName("total")]
        public uint Total { get; set; }
    }
}