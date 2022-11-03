using System.Collections.Generic;
using System.Text.Json.Serialization;
using Monero.Client.Daemon.POD.Requests;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Requests;

namespace Monero.Client.Network
{
    /// <summary>
    /// Used for json_rpc interface commands.
    /// </summary>
    internal class GenericRequestParameters
    {
        [JsonPropertyName("height")]
        public ulong? Height { get; set; } = null;
        [JsonPropertyName("hash")]
        public string Hash { get; set; } = null;
        [JsonPropertyName("start_height")]
        public uint? Start_height { get; set; } = null;
        [JsonPropertyName("end_height")]
        public uint? End_height { get; set; } = null;
        [JsonPropertyName("txids")]
        public IEnumerable<string> Txids { get; set; } = null;
        [JsonPropertyName("amounts")]
        public IEnumerable<ulong> Amounts { get; set; } = null;
        [JsonPropertyName("min_count")]
        public uint? Min_count { get; set; } = null;
        [JsonPropertyName("max_count")]
        public uint? Max_count { get; set; } = null;
        [JsonPropertyName("unlocked")]
        public bool? Unlocked { get; set; } = null;
        [JsonPropertyName("recent_cutoff")]
        public uint? Recent_cutoff { get; set; } = null;
        [JsonPropertyName("count")]
        public uint? Count { get; set; } = null;
        [JsonPropertyName("grace_blocks")]
        public uint? Grace_blocks { get; set; } = null;
        [JsonPropertyName("cumulative")]
        public bool? Cumulative { get; set; } = null;
        [JsonPropertyName("from_height")]
        public ulong? From_height { get; set; } = null;
        [JsonPropertyName("to_height")]
        public ulong? To_height { get; set; } = null;
        [JsonPropertyName("bans")]
        public List<NodeBan> Bans { get; set; } = null;
        [JsonPropertyName("account_index")]
        public uint? Account_index { get; set; } = null;
        [JsonPropertyName("address_indices")]
        public IEnumerable<uint> Address_indices { get; set; } = null;
        [JsonPropertyName("address")]
        public string Address { get; set; } = null;
        [JsonPropertyName("amount")]
        public ulong? Amount { get; set; } = null;
        [JsonPropertyName("label")]
        public string Label { get; set; } = null;
        [JsonPropertyName("tx_description")]
        public string Tx_description { get; set; } = null;
        [JsonPropertyName("payment_id")]
        public string Payment_id { get; set; } = null;
        [JsonPropertyName("recipient_name")]
        public string Recipient_name { get; set; } = null;
        [JsonPropertyName("index")]
        public AddressIndexParameter Index { get; set; } = null;
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = null;
        [JsonPropertyName("entries")]
        public IEnumerable<uint> Entries { get; set; } = null;
        [JsonPropertyName("accounts")]
        public IEnumerable<uint> Accounts { get; set; } = null;
        [JsonPropertyName("multisig_info")]
        public IEnumerable<string> Multisig_info { get; set; } = null;
        [JsonPropertyName("threshold")]
        public uint? Threshold { get; set; } = null;
        [JsonPropertyName("description")]
        public string Description { get; set; } = null;
        [JsonPropertyName("destinations")]
        public IEnumerable<FundTransferParameter> Destinations { get; set; } = null;
        [JsonPropertyName("subaddr_indices")]
        public IEnumerable<uint> Subaddr_indices { get; set; } = null;
        [JsonPropertyName("priority")]
        public uint? Priority { get; set; } = null;
        [JsonPropertyName("mixin")]
        public uint? Mixin { get; set; } = null;
        [JsonPropertyName("ring_size")]
        public uint? Ring_size { get; set; } = null;
        [JsonPropertyName("unlock_time")]
        public ulong? Unlock_time { get; set; } = null;
        [JsonPropertyName("get_tx_key")]
        public bool? Get_tx_key { get; set; } = null;
        [JsonPropertyName("get_tx_keys")]
        public bool? Get_tx_keys { get; set; } = null;
        [JsonPropertyName("get_tx_hex")]
        public bool? Get_tx_hex { get; set; } = null;
        [JsonPropertyName("get_tx_metadata")]
        public bool? Get_tx_metadata { get; set; } = null;
        [JsonPropertyName("new_algorithm")]
        public bool? New_algorithm { get; set; } = null;
        [JsonPropertyName("unsigned_txset")]
        public string Unsigned_txset { get; set; } = null;
        [JsonPropertyName("export_raw")]
        public bool? Export_raw { get; set; } = null;
        [JsonPropertyName("tx_data_hex")]
        public string Tx_data_hex { get; set; } = null;
        [JsonPropertyName("do_not_relay")]
        public bool? Do_not_relay { get; set; } = null;
        [JsonPropertyName("below_amount")]
        public ulong? Below_amount { get; set; } = null;
        [JsonPropertyName("transfer_type")]
        public string Transfer_type { get; set; } = null;
        [JsonPropertyName("verbose")]
        public bool? Verbose { get; set; } = null;
        [JsonPropertyName("key_type")]
        public string Key_type { get; set; } = null;
        [JsonPropertyName("notes")]
        public IEnumerable<string> Notes { get; set; } = null;
        [JsonPropertyName("txid")]
        public string Txid { get; set; } = null;
        [JsonPropertyName("in")]
        public bool? In { get; set; } = null;
        [JsonPropertyName("out")]
        public bool? Out { get; set; } = null;
        [JsonPropertyName("pending")]
        public bool? Pending { get; set; } = null;
        [JsonPropertyName("failed")]
        public bool? Failed { get; set; } = null;
        [JsonPropertyName("pool")]
        public bool? Pool { get; set; } = null;
        [JsonPropertyName("min_height")]
        public ulong? Min_height { get; set; } = null;
        [JsonPropertyName("max_height")]
        public ulong? Max_height { get; set; } = null;
        [JsonPropertyName("filter_by_height")]
        public bool? Filter_by_height { get; set; } = null;
        [JsonPropertyName("data")]
        public string Data { get; set; } = null;
        [JsonPropertyName("signature")]
        public string Signature { get; set; } = null;
        [JsonPropertyName("signed_key_images")]
        public List<SignedKeyImage> Signed_key_images { get; set; } = null;
        [JsonPropertyName("uri")]
        public string Uri { get; set; } = null;
        [JsonPropertyName("filename")]
        public string Filename { get; set; } = null;
        [JsonPropertyName("password")]
        public string Password { get; set; } = null;
        [JsonPropertyName("language")]
        public string Language { get; set; } = null;
        [JsonPropertyName("old_password")]
        public string Old_password { get; set; } = null;
        [JsonPropertyName("new_password")]
        public string New_password { get; set; } = null;
        [JsonPropertyName("all_accounts")]
        public bool? All_accounts { get; set; } = null;
        [JsonPropertyName("strict")]
        public bool? Strict { get; set; } = null;
        [JsonPropertyName("multisig_txset")]
        public string Multisig_txset { get; set; } = null;
        [JsonPropertyName("hex")]
        public string Hex { get; set; } = null;
        [JsonPropertyName("reserve_size")]
        public ulong? Reserve_size { get; set; } = null;
        [JsonPropertyName("wallet_address")]
        public string Wallet_address { get; set; } = null;
        [JsonPropertyName("prev_block")]
        public string Prev_block { get; set; } = null;
        [JsonPropertyName("extra_nonce")]
        public string Extra_nonce { get; set; } = null;
        [JsonPropertyName("binary")]
        public bool? Binary { get; set; } = null;
        [JsonPropertyName("compress")]
        public bool? Compress { get; set; } = null;
        [JsonPropertyName("check")]
        public bool? Check { get; set; } = null;
        [JsonPropertyName("key")]
        public string Key { get; set; } = null;
        [JsonPropertyName("value")]
        public string Value { get; set; } = null;
        [JsonPropertyName("any_net_type")]
        public bool? AnyNetType { get; set; } = null;
        [JsonPropertyName("allow_openalias")]
        public bool? AllowOpenAlias { get; set; } = null;
    }
}