using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
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
            bool equalAmounts = this.TransactionHashes.Count == this.Amounts.Count && this.Amounts.Count == this.Fees.Count;
            var sb = new StringBuilder();
            if (equalAmounts)
            {
                for (int transferNumber = 0; transferNumber < this.TransactionHashes.Count; ++transferNumber)
                {
                    sb.AppendLine($"Sent {PriceUtilities.PiconeroToMonero(this.Amounts[transferNumber]).ToString(PriceFormat.MoneroPrecision)} with a fee of {PriceUtilities.PiconeroToMonero(this.Fees[transferNumber]).ToString(PriceFormat.MoneroPrecision)} [{this.TransactionHashes[transferNumber]}]");
                }
            }

            return sb.ToString();
        }
    }
}