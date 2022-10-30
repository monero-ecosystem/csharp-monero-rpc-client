using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Daemon.POD.Responses
{
    internal class OutputHistogramResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public OutputHistogramResult Result { get; set; }
    }
}