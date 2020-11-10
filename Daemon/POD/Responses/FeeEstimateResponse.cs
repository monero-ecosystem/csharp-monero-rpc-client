using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class FeeEstimateResponse : RpcResponse
    {
        public FeeEstimateResult result { get; set; }
    }

    public class FeeEstimateResult
    {
        public uint credits { get; set; }
        public ulong fee { get; set; }
        public uint quantization_mask { get; set; }
        public string status { get; set; }
        public string top_hash { get; set; }
        public bool untrusted { get; set; }
    }
}