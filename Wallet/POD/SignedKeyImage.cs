using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD
{
    public class SignedKeyImage : KeyImage
    {
        public string signature { get; set; }
    }
}