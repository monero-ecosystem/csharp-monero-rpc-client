using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class CreateAccountResponse : RpcResponse
    {
        public CreateAccountResult result { get; set; }
    }

    public class CreateAccountResult
    {
        public uint account_index { get; set; }
        public string address { get; set; }
    }
}