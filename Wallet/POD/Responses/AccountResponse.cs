﻿using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class AccountResponse : RpcResponse
    {
        public AccountResult result { get; set; }
    }

    public class AccountResult
    {
        public List<SubaddressDetails> subaddress_accounts { get; set; } = new List<SubaddressDetails>();
        public ulong total_balance { get; set; }
        public ulong total_unlocked_balance { get; set; }
    }
}