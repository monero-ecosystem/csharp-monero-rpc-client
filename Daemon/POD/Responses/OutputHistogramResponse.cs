using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Daemon.POD.Responses
{
    public class OutputHistogramResponse : RpcResponse
    {
        public OutputHistogramResult result { get; set; }
    }

    public class OutputHistogramResult
    {
        public uint credits { get; set; }
        public List<Histogram> histogram { get; set; } = new List<Histogram>();
        public string status { get; set; }
        public bool untrusted { get; set; }
        public string top_hash { get; set; }
    }

    public class Histogram
    {
        public ulong amount { get; set; }
        public uint recent_instances { get; set; }
        public uint total_instances { get; set; }
        public uint unlocked_instances { get; set; }
    }
}