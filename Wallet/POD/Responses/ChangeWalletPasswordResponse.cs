﻿using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ChangeWalletPasswordResponse : RpcResponse
    {
        public ChangeWalletPasswordResult result { get; set; }
    }

    public class ChangeWalletPasswordResult
    {
        // ...
    }
}