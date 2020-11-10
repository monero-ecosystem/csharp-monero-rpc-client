using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ExportMultiSigInfoResponse : RpcResponse
    {
        public ExportMultiSigInfoResult result { get; set; }
    }

    public class ExportMultiSigInfoResult
    {
        public string info { get; set; }
    }
}