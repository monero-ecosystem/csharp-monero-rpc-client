using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class SignMultiSigTransactionResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public SignMultiSigTransaction Result { get; set; }
    }

    public class SignMultiSigTransaction
    {
        [JsonPropertyName("tx_data_hex")]
        public string TransactionDataHex { get; set; }
        [JsonPropertyName("tx_hash_list")]
        public List<string> TransactionHashes { get; set; } = new List<string>();
        public override string ToString()
        {
            return $"DataHex: {TransactionDataHex} - TxHashes: {string.Join(" ", TransactionHashes)}";
        }
    }
}