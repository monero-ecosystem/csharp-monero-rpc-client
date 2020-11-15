using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Network
{
    internal class AnonymousRequest : Request
    {
        public string method { get; set; }
        public dynamic @params { get; set; }
    }
}