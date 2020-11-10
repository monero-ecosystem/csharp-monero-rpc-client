using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
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