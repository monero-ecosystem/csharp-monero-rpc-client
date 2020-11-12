using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportOutputsResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ImportOutputsResult Result { get; set; }
    }

    public class ImportOutputsResult
    {
        [JsonPropertyName("num_imported")]
        public ulong NumImported { get; set; }
    }
}