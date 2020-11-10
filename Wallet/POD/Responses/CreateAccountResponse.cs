﻿using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class CreateAccountResponse : RpcResponse
    {
        public CreateAccountResult result { get; set; }
    }

    public class CreateAccountResult
    {
        public uint account_index { get; set; }
        public string address { get; set; }
    }
}