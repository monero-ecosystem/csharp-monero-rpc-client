using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD
{
    public class SubaddressDetails
    {
        public uint account_index { get; set; }
        public ulong balance { get; set; }
        public string base_address { get; set; }
        public string label { get; set; }
        public string tag { get; set; }
        public ulong unlocked_balance { get; set; }
    }
}