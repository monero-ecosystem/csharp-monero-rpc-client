using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ImportOutputsResponse : RpcResponse
    {
        public ImportOutputsResult result { get; set; }
    }

    public class ImportOutputsResult
    {
        public uint num_imported { get; set; }
    }
}