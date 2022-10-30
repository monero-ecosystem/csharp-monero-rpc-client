using Monero.Client.Network;
using System.Text.Json.Serialization;

namespace Monero.Client.Wallet.POD.Responses
{

    public class RefreshWallet
    {
        [JsonPropertyName("blocks_fetched")]
        public ulong BlocksFetched { get; set; }
        [JsonPropertyName("received_money")]
        public bool ReceivedMoney { get; set; }
        public override string ToString()
        {
            return $"{(this.ReceivedMoney ? "Money Received" : "No Money Received")} (Blocks Fetched: {this.BlocksFetched})";
        }
    }
}