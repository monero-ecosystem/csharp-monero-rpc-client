using Monero.Client.Network;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;
using Monero.Client.Utilities;

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
        public override string ToString()
        {
            bool equalAmounts = TransactionHashes.Count == Amounts.Count && Amounts.Count == Fees.Count;
            var sb = new StringBuilder();
            if (equalAmounts)
            {
                for (int transferNumber = 0; transferNumber < TransactionHashes.Count; ++transferNumber)
                    sb.AppendLine($"Sent {PriceUtilities.PiconeroToMonero(Amounts[transferNumber]):N12} with a fee of {PriceUtilities.PiconeroToMonero(Fees[transferNumber]):N12} [{TransactionHashes[transferNumber]}]");
            }
            return sb.ToString();
        }
    }
}