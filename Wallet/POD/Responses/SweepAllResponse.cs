using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class SweepAllResponse : RpcResponse
    {
        // Result is formatted the same as FundTransferResponse.
        public FundTransferSplitResult result { get; set; }
    }
}