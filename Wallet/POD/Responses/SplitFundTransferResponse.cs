using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SplitFundTransferResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SplitFundTransfer Result { get; set; }
    }

    public class SplitFundTransfer
    {
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
        [JsonPropertyName("tx_key_list")]
        public List<string> TransactionKeys { get; set; } = new List<string>();
        [JsonPropertyName("amount_list")]
        public List<ulong> Amounts { get; set; } = new List<ulong>();
        [JsonPropertyName("fee_list")]
        public List<ulong> Fees { get; set; } = new List<ulong>();
        [JsonPropertyName("tx_metadata_list")]
        public List<string> TransactionMetadata { get; set; } = new List<string>();
        [JsonPropertyName("multisig_txset")]
        public string MultiSigTransactionSet { get; set; }
        [JsonPropertyName("unsigned_txset")]
        public string UnsignedTransactionSet { get; set; }
        [JsonPropertyName("weight_list")]
        public List<ulong> Weights { get; set; } = new List<ulong>();
    }
}