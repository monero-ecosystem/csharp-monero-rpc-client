using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ExportKeyImagesResponse : RpcResponse
    {
        public ExportKeyImagesResult result { get; set; }
    }

    public class ExportKeyImagesResult
    {
        public List<SignedKeyImage> signed_key_images = new List<SignedKeyImage>();
    }
}