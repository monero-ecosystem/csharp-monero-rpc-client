using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ExportOutputsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ExportOutputs Result { get; set; }
    }

    public class ExportOutputs
    {
        [JsonPropertyName("outputs_data_hex")]
        public string OutputsDataHex { get; set; }
    }
}