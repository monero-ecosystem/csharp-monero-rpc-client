using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Constants;
using Monero.Client.Network;
using Monero.Client.Utilities;

namespace Monero.Client.Wallet.POD.Responses
{
    public class Transfer
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("amounts")]
        public List<ulong> Amounts { get; set; } = new List<ulong>();
        [JsonPropertyName("destinations")]
        public List<TransferDestination> Destinations { get; set; } = new List<TransferDestination>();
        [JsonPropertyName("confirmations")]
        public ulong Confirmations { get; set; }
        [JsonPropertyName("fee")]
        public ulong Fee { get; set; }
        [JsonPropertyName("double_spend_seen")]
        public bool IsDoubleSpendSeen { get; set; }
        [JsonPropertyName("height")]
        public ulong Height { get; set; }
        [JsonPropertyName("locked")]
        public bool IsLocked { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
        [JsonPropertyName("payment_id")]
        public string PaymentID { get; set; }
        [JsonPropertyName("subaddr_index")]
        public SubaddressIndex SubaddressIndex { get; set; }
        [JsonPropertyName("subaddr_indices")]
        public List<SubaddressIndex> SubaddressIndices { get; set; } = new List<SubaddressIndex>();
        [JsonPropertyName("suggested_confirmations_threshold")]
        public ulong SuggestedConfirmationsThreshold { get; set; }
        [JsonPropertyName("timestamp")]
        public ulong Timestamp { get; set; }
        [JsonPropertyName("txid")]
        public string TransactionID { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("unlock_time")]
        public ulong UnlockTime { get; set; }
        [JsonIgnore]
        public TimeSpan EstimatedTimeTillUnlock
        {
            get
            {
                return BlockchainNetworkDefaults.AverageBlockTime * (BlockchainNetworkDefaults.BaseBlockUnlockThreshold + this.UnlockTime);
            }
        }

        [JsonIgnore]
        public DateTime DateTime
        {
            get
            {
                return DateTime.UnixEpoch.AddSeconds(this.Timestamp);
            }
        }

        public override string ToString()
        {
            return $"[{this.Height}] [{this.TransactionID}] ({this.DateTime.ToString(DateFormat.DateTimeFormat)}) - {this.Address} - {PriceUtilities.PiconeroToMonero(this.Amount).ToString(PriceFormat.MoneroPrecision)} - {PriceUtilities.PiconeroToMonero(this.Fee)} - Confirmations: {this.Confirmations}";
        }
    }
}