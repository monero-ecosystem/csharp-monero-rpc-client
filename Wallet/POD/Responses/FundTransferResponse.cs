using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using Monero.Client.Network;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class FundTransferResponse : RpcResponse
    {
        [JsonPropertyName("address")]
        public FundTransfer Result { get; set; }
    }

    public class FundTransfer
    {
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("fee")]
        public ulong Fee { get; set; }
        [JsonPropertyName("multisig_txset")]
        public string MultiSigTxSet { get; set; }
        [JsonPropertyName("tx_blob")]
        public string TransactionBlob { get; set; }
        [JsonPropertyName("tx_hash")]
        public string TransactionHash { get; set; }
        [JsonPropertyName("tx_key")]
        public string TransactionKey { get; set; }
        [JsonPropertyName("tx_metadata")]
        public string TransactionMetadata { get; set; }
        [JsonPropertyName("unsigned_txset")]
        public string UnsignedTxSet { get; set; }
        [JsonPropertyName("weight")]
        public ulong Weight { get; set; }
    }
}