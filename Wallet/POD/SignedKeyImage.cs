using System;
using System.Collections.Generic;
using System.Text;

namespace MoneroClient.Wallet.POD
{
    public class SignedKeyImage : KeyImage
    {
        public string signature { get; set; }
    }
}