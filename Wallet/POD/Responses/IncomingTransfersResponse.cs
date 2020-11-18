using Monero.Client.Network;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{
    internal class IncomingTransfersResponse : RpcResponse
    {
        [JsonPropertyName("result")]
        public IncomingTransfers Result { get; set; }
    }

    public class IncomingTransfers
    {
        [JsonPropertyName("transfers")]
        public List<IncomingTransfer> Transfers { get; set; } = new List<IncomingTransfer>();
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
    }
}