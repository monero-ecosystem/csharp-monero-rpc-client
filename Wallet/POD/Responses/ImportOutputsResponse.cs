using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ImportOutputsResponse : RpcResponse
    {
        public ImportOutputsResult result { get; set; }
    }

    public class ImportOutputsResult
    {
        public uint num_imported { get; set; }
    }
}