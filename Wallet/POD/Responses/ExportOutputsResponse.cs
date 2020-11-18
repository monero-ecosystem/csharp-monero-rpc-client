﻿using Monero.Client.Network;
using System.Text.Json.Serialization;

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