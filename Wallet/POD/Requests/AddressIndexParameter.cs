using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD.Requests
{
    internal class AddressIndexParameter
    {
        public uint major { get; set; }
        public uint minor { get; set; }
    }
}