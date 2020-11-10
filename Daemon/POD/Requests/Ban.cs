using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Daemon.POD.Requests
{
    public class Ban
    {
        public string host { get; set; }
        public uint ip { get; set; }
        public bool ban { get; set; }
        public uint seconds { get; set; }
    }
}