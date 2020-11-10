using System;
using System.Collections.Generic;
using System.Text;

namespace Monero.Client.Wallet.POD.Requests
{
    internal class WalletRequestParameters
    {
        public uint? account_index = null;
        public IEnumerable<uint> address_indices = null;
        public string address = null;
        public ulong? amount = null;
        public string label = null;
        public string tx_description = null;
        public string payment_id = null;
        public string recipient_name = null;
        public AddressIndexParameter index = null;
        public string tag = null;
        public IEnumerable<uint> entries = null;
        public IEnumerable<uint> accounts = null;
        public IEnumerable<string> multisig_info = null;
        public uint? threshold = null;
        public string description = null;
        public IEnumerable<FundTransferParameter> destinations = null;
        public IEnumerable<uint> subaddr_indices = null;
        public uint? priority = null;
        public uint? mixin = null;
        public uint? ring_size = null;
        public ulong? unlock_time = null;
        public bool? get_tx_key = null;
        public bool? get_tx_keys = null;
        public bool? get_tx_hex = null;
        public bool? get_tx_metadata = null;
        public bool? new_algorithm = null;
        public string unsigned_txset = null;
        public bool? export_raw = null;
        public string tx_data_hex = null;
        public bool? do_not_relay = null;
        public ulong? below_amount = null;
        public string transfer_type = null;
        public bool? verbose = null;
        public string key_type = null;
        public IEnumerable<string> txids = null;
        public IEnumerable<string> notes = null;
        public string txid = null;
        public string tx_key = null;
        public bool? @in = null;
        public bool? @out = null;
        public bool? pending = null;
        public bool? failed = null;
        public bool? pool = null;
        public uint? min_height = null;
        public uint? max_height = null;
        public bool? filter_by_height = null;
        public string data = null;
        public string signature = null;
        public List<SignedKeyImage> signed_key_images = null;
        public string uri = null;
        public uint? start_height = null;
        public string filename = null;
        public string password = null;
        public string language = null;
        public string old_password = null;
        public string new_password = null;
    }
}   
