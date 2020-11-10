using System;
using System.Collections.Generic;
using System.Text;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    public class LanguagesResponse : RpcResponse
    {
        public LanguagesResult result { get; set; }
    }

    public class LanguagesResult
    {
        public List<string> languages = new List<string>();
        public List<string> languages_local = new List<string>();
    }
}