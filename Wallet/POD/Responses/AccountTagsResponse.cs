using System;
using System.Collections.Generic;
using System.Text;

using MoneroClient.Network;

namespace MoneroClient.Wallet.POD.Responses
{
    public class AccountTagsResponse : RpcResponse
    {
        public AccountTagsResult result { get; set; }
    }

    public class AccountTagsResult
    {
        public List<AccountTag> account_tags = new List<AccountTag>();
    }

    public class AccountTag
    {
        public List<uint> accounts { get; set; } = new List<uint>();
        public string tag { get; set; }
        public string label { get; set; }
    }
}