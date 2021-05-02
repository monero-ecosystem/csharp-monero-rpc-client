﻿using Monero.Client.Daemon.POD.Requests;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Requests;
using System.Collections.Generic;

namespace Monero.Client.Network
{
    /// <summary>
    /// Used for json_rpc interface commands.
    /// </summary>
    internal class GenericRequestParameters
    {
        public ulong? height { get; set; } = null;
        public string hash { get; set; } = null;
        public uint? start_height { get; set; } = null;
        public uint? end_height { get; set; } = null;
        public IEnumerable<string> txids { get; set; } = null;
        public IEnumerable<ulong> amounts { get; set; } = null;
        public uint? min_count { get; set; } = null;
        public uint? max_count { get; set; } = null;
        public bool? unlocked { get; set; } = null;
        public uint? recent_cutoff { get; set; } = null;
        public uint? count { get; set; } = null;
        public uint? grace_blocks { get; set; } = null;
        public bool? cumulative { get; set; } = null;
        public ulong? from_height { get; set; } = null;
        public ulong? to_height { get; set; } = null;
        public List<NodeBan> bans { get; set; } = null;
        public uint? account_index { get; set; } = null;
        public IEnumerable<uint> address_indices { get; set; } = null;
        public string address { get; set; } = null;
        public ulong? amount { get; set; } = null;
        public string label { get; set; } = null;
        public string tx_description { get; set; } = null;
        public string payment_id { get; set; } = null;
        public string recipient_name { get; set; } = null;
        public AddressIndexParameter index { get; set; } = null;
        public string tag { get; set; } = null;
        public IEnumerable<uint> entries { get; set; } = null;
        public IEnumerable<uint> accounts { get; set; } = null;
        public IEnumerable<string> multisig_info { get; set; } = null;
        public uint? threshold { get; set; } = null;
        public string description { get; set; } = null;
        public IEnumerable<FundTransferParameter> destinations { get; set; } = null;
        public IEnumerable<uint> subaddr_indices { get; set; } = null;
        public uint? priority { get; set; } = null;
        public uint? mixin { get; set; } = null;
        public uint? ring_size { get; set; } = null;
        public ulong? unlock_time { get; set; } = null;
        public bool? get_tx_key { get; set; } = null;
        public bool? get_tx_keys { get; set; } = null;
        public bool? get_tx_hex { get; set; } = null;
        public bool? get_tx_metadata { get; set; } = null;
        public bool? new_algorithm { get; set; } = null;
        public string unsigned_txset { get; set; } = null;
        public bool? export_raw { get; set; } = null;
        public string tx_data_hex { get; set; } = null;
        public bool? do_not_relay { get; set; } = null;
        public ulong? below_amount { get; set; } = null;
        public string transfer_type { get; set; } = null;
        public bool? verbose { get; set; } = null;
        public string key_type { get; set; } = null;
        public IEnumerable<string> notes { get; set; } = null;
        public string txid { get; set; } = null;
        public bool? @in { get; set; } = null;
        public bool? @out { get; set; } = null;
        public bool? pending { get; set; } = null;
        public bool? failed { get; set; } = null;
        public bool? pool { get; set; } = null;
        public ulong? min_height { get; set; } = null;
        public ulong? max_height { get; set; } = null;
        public bool? filter_by_height { get; set; } = null;
        public string data { get; set; } = null;
        public string signature { get; set; } = null;
        public List<SignedKeyImage> signed_key_images { get; set; } = null;
        public string uri { get; set; } = null;
        public string filename { get; set; } = null;
        public string password { get; set; } = null;
        public string language { get; set; } = null;
        public string old_password { get; set; } = null;
        public string new_password { get; set; } = null;
        public bool? all_accounts { get; set; } = null;
        public bool? strict { get; set; } = null;
        public string multisig_txset { get; set; } = null;
        public string hex { get; set; } = null;
        public ulong? reserve_size { get; set; } = null;
        public string wallet_address { get; set; } = null;
        public string prev_block { get; set; } = null;
        public string extra_nonce { get; set; } = null;
        public bool? binary { get; set; } = null;
        public bool? compress { get; set; } = null;
        public bool? check { get; set; } = null;
        public string key { get; set; } = null;
        public string value { get; set; } = null;
    }

    /// <summary>
    /// Used for non json_rpc interface commands.
    /// </summary>
    internal class CustomRequestParameters : GenericRequestParameters
    {
        public IEnumerable<string> txs_hashes { get; set; } = null;
    }
}