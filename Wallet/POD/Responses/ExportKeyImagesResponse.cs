using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class ExportKeyImagesResponse : RpcResponse
    {
        public ExportKeyImagesResult result { get; set; }
    }

    public class ExportKeyImagesResult
    {
        public List<SignedKeyImage> signed_key_images = new List<SignedKeyImage>();
    }
}