using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Daemon.POD.Requests
{
    internal class DaemonRequestParameters
    {
        public uint? height { get; set; } = null;
        public string hash { get; set; } = null;
        public uint? start_height { get; set; } = null;
        public uint? end_height { get; set; } = null;
        public IEnumerable<string> txids { get; set; } = null;
        public IEnumerable<ulong> amounts { get; set; } = null;
        public uint? min_count { get; set; } = null;
        public uint? max_count { get; set; } = null;
        public bool? unlocked { get; set; } = null;
        public uint? recent_cutoff { get; set; } = null;
        public uint? count { get; set; } = null;
        public uint? grace_blocks { get; set; } = null;
        public bool? cumulative { get; set; } = null;
        public uint? from_height { get; set; } = null;
        public uint? to_height { get; set; } = null;
        public List<NodeBan> bans { get; set; } = null;
    }
}