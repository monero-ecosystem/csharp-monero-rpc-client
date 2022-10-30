using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

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
            bool hasIncomingTransfers = this.IncomingTransfers.Count > 0, hasOutgoingTransfers = this.OutgoingTransfers.Count > 0, hasPendingTransfers = this.PendingTransfers.Count > 0,
                hasFailedTransfers = this.FailedTransfers.Count > 0, hasPooledTransfers = this.PooledTransfers.Count > 0;

            if (hasIncomingTransfers)
            {
                sb.AppendLine("Incoming Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, this.IncomingTransfers));
                sb.AppendLine();
            }

            if (hasOutgoingTransfers)
            {
                sb.AppendLine("Outgoing Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, this.OutgoingTransfers));
                sb.AppendLine();
            }

            if (hasPendingTransfers)
            {
                sb.AppendLine("Pending Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, this.PendingTransfers));
                sb.AppendLine();
            }

            if (hasFailedTransfers)
            {
                sb.AppendLine("Failed Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, this.FailedTransfers));
                sb.AppendLine();
            }

            if (hasPooledTransfers)
            {
                sb.AppendLine("Pooled Transfers:");
                sb.AppendLine(string.Join(Environment.NewLine, this.PooledTransfers));
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}