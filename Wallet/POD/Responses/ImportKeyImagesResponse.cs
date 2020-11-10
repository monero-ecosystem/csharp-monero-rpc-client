using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ImportKeyImagesResponse : RpcResponse
    {
        public ImportKeyImagesResult result { get; set; }
    }

    public class ImportKeyImagesResult
    {
        public uint height { get; set; }
        public ulong spent { get; set; }
        public ulong unspent { get; set; }
    }
}