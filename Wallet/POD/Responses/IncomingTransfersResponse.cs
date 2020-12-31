using Monero.Client.Network;
using Monero.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class IncomingTransfersResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public IncomingTransfersResult Result { get; set; }
    }

    internal class IncomingTransfersResult
    {
        [JsonPropertyName("transfers")]
        public List<IncomingTransfer> Transfers { get; set; } = new List<IncomingTransfer>();
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Transfers);
        }
    }

    public class IncomingTransfer
    {
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }
        [JsonPropertyName("block_height")]
        public ulong BlockHeight { get; set; }
        [JsonPropertyName("global_index")]
        public ulong GlobalIndex { get; set; }
        [JsonPropertyName("frozen")]
        public bool IsFrozen { get; set; }
        [JsonPropertyName("key_image")]
        public string KeyImage { get; set; }
        [JsonPropertyName("spent")]
        public bool IsSpent { get; set; }
        [JsonPropertyName("subaddr_index")]
        public SubaddressIndex SubaddressIndex { get; set; }
        [JsonPropertyName("tx_hash")]
        public string TransactionHash { get; set; }
        [JsonPropertyName("unlocked")]
        public bool IsUnlocked { get; set; }
        public override string ToString()
        {
            return $"[{BlockHeight}] - {TransactionHash} - {PriceUtilities.PiconeroToMonero(Amount).ToString(PriceFormat.MoneroPrecision)} - {(IsSpent ? "Spent" : "Unspent")} - {(IsUnlocked ? "Unlocked" : "Locked")}";
        }
    }
}