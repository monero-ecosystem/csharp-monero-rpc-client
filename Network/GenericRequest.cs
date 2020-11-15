using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Network
{
    internal class GenericRequest : Request
    {
        public string method { get; set; }
        public GenericRequestParameters @params { get; set; }
    }
}