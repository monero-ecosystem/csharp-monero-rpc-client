using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Daemon.POD.Responses
{
    public class OutputHistogramResponse : RpcResponse
    {
        public OutputHistogramResult result { get; set; }
    }

    public class OutputHistogramResult
    {
        public uint credits { get; set; }
        public List<Histogram> histogram { get; set; }
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