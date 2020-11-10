using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ImportMultiSigInfoResponse : RpcResponse
    {
        public ImportMultiSigInfoResult result { get; set; }
    }

    public class ImportMultiSigInfoResult
    {
        public uint n_outputs { get; set; }
    }
}