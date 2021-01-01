using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SubmitMultiSigTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SubmitMultiSigResult Result { get; set; }
    }

    internal class SubmitMultiSigResult
    {
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
        public override string ToString()
        {
            return string.Join(" ", TransactionHashes);
        }
    }
}