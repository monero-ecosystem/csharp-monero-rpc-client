using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ExportOutputsResponse : RpcResponse
    {
        public ExportOutputsResult result { get; set; }
    }

    public class ExportOutputsResult
    {
        public string outputs_data_hex { get; set; }
    }
}