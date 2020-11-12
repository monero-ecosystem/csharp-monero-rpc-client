using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportMultiSigInfoResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ImportMultiSigInformation Result { get; set; }
    }

    public class ImportMultiSigInformation
    {
        [JsonPropertyName("n_outputs")]
        public uint N_Outputs { get; set; }
    }
}