using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class SweepAllResponse : RpcResponse
    {
        // Result is formatted the same as FundTransferResponse.
        public FundTransferSplitResult result { get; set; }
    }
}