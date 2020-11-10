using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class BalanceResponse : RpcResponse
    {
        public BalanceResult result { get; set; }

    }

    public class BalanceResult
    {
        public ulong balance { get; set; }
        public uint blocks_to_unlock { get; set; }
        public bool multisig_import_needed { get; set; }
        public List<AddressDetails> per_subaddress { get; set; } = new List<AddressDetails>();
        public ulong time_to_unlock { get; set; }
        public ulong unlocked_balance { get; set; }
    }
}