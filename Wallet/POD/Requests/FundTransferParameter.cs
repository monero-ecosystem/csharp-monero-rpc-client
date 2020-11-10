using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD.Requests
{
    internal class FundTransferParameter
    {
        public ulong amount { get; set; }
        public string address { get; set; }
    }
}