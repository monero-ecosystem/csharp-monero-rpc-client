﻿using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportMultiSigInfoResponse : RpcResponse
    {
        public ImportMultiSigInfoResult result { get; set; }
    }

    public class ImportMultiSigInfoResult
    {
        public uint n_outputs { get; set; }
    }
}