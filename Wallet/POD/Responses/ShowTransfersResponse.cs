using Monero.Client.Network;
using Monero.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class ShowTransfersResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public ShowTransfers Result { get; set; }
    }

    public class ShowTransfers
    {
        [JsonPropertyName("in")]
        public List<Transfer> IncomingTransfers { get; set; } = new List<Transfer>();
        [JsonPropertyName("out")]
        public List<Transfer> OutgoingTransfers { get; set; } = new List<Transfer>();
        [JsonPropertyName("pending")]
        public List<Transfer> PendingTransfers { get; set; } = new List<Transfer>();
        [JsonPropertyName("failed")]
        public List<Transfer> FailedTransfers { get; set; } = new List<Transfer>();
        [JsonPropertyName("pool")]
        public List<Transfer> PooledTransfers { get; set; } = new List<Transfer>();
        public override string ToString()
        {
            var sb = new StringBuilder();
            bool hasIncomingTransfers = IncomingTransfers.Count > 0, hasOutgoingTransfers = OutgoingTransfers.Count > 0, hasPendingTransfers = PendingTransfers.Count > 0,
                hasFailedTransfers = FailedTransfers.Count > 0, hasPooledTransfers = PooledTransfers.Count > 0;
            if (hasIncomingTransfers)
            {
                sb.AppendLine("Incoming Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, IncomingTransfers));
                sb.AppendLine();
            }
            if (hasOutgoingTransfers)
            {
                sb.AppendLine("Outgoing Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, OutgoingTransfers));
                sb.AppendLine();
            }
            if (hasPendingTransfers)
            {
                sb.AppendLine("Pending Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, PendingTransfers));
                sb.AppendLine();
            }
            if (hasFailedTransfers)
            {
                sb.AppendLine("Failed Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, FailedTransfers));
                sb.AppendLine();
            }
            if (hasPooledTransfers)
            {
                sb.AppendLine("Pooled Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, PooledTransfers));
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    public class TransferDestination
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
    }

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
        [JsonIgnore()]
        public TimeSpan EstimatedTimeTillUnlock
        {
            get
            {
                return BlockchainNetworkDefaults.AverageBlockTime * (BlockchainNetworkDefaults.BaseBlockUnlockThreshold + this.UnlockTime);
            }
        }
        [JsonIgnore()]
        public DateTime DateTime
        {
            get
            {
                return DateTime.UnixEpoch.AddSeconds(this.Timestamp);
            }
        }

        public override string ToString()
        {
            return $"[{Height}] [{TransactionID}] ({DateTime.ToString(DateFormat.DateTimeFormat)}) - {Address} - {PriceUtilities.PiconeroToMonero(Amount).ToString(PriceFormat.MoneroPrecision)} - {PriceUtilities.PiconeroToMonero(Fee)} - Confirmations: {Confirmations}";
        }
    }
}