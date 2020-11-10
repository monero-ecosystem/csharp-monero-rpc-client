using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MoneroClient.Wallet.POD
{
    public class AddressDetails
    {
        public uint account_index { get; set; }
        public string address { get; set; }
        public uint address_index { get; set; }
        public ulong balance { get; set; }
        public uint blocks_to_unlock { get; set; }
        public string label { get; set; }
        public uint num_unspect_outputs { get; set; }
        public ulong time_to_unlock { get; set; }
        public ulong unlocked_balance { get; set; }
    }
}