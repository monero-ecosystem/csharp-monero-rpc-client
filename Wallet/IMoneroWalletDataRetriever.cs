using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Monero.Client.Wallet.POD.Requests;
using Monero.Client.Wallet.POD.Responses;
using Monero.Client.Wallet.POD;

namespace Monero.Client.Wallet
{
    internal interface IMoneroWalletDataRetriever : IDisposable
    {
        Task<MoneroWalletDataRetrieverResponse> GetBalanceAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetBalanceAsync(uint account_index, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetAddressAsync(uint account_index, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetAddressAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetAddressIndexAsync(string address, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> CreateAddressAsync(uint account_index, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> CreateAddressAsync(uint account_index, string label, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> LabelAddressAsync(uint major_index, uint minor_index, string label, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetAccountsAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetAccountsAsync(string tag, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> CreateAccountAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> CreateAccountAsync(string label, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> LabelAccountAsync(uint account_index, string label, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetAccountTagsAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetHeightAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, uint unlock_time = 0, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool new_algorithm = true, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, bool new_algorithm = true, uint unlock_time = 0, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SignTransferAsync(string unsigned_txset, bool export_raw = false, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SubmitTransferAsync(string tx_data_hex, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SweepDustAsync(bool get_tx_keys, bool get_tx_hex, bool get_tx_metadata, bool do_not_relay = false, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SweepAllAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time = 0, ulong below_amount = ulong.MaxValue, bool get_tx_keys = true, bool get_tx_hex = true, bool get_tx_metadata = true, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SaveWalletAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetIncomingTransfersAsync(TransferType transfer_type, bool return_key_image = false, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, bool return_key_image = false, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, IEnumerable<uint> subaddr_indices, bool return_key_image = false, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetPrivateKey(KeyType key_type, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> StopWalletAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetTransactionKeyAsync(string txid, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> CheckTransactionKeyAsync(string txid, string tx_key, string address, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint min_height, uint max_height, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetTransferByTxidAsync(string txid, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetTransferByTxidAsync(string txid, uint account_index, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SignAsync(string data, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> VerifyAsync(string data, string address, string signature, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> ExportOutputsAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> ImportOutputsAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> ExportKeyImagesAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> ImportKeyImagesAsync(IEnumerable<(string key_image, string signature)> signed_key_images, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> MakeUriAsync(string address, ulong amount, string recipient_name, string tx_description = null, string payment_id = null, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> ParseUriAsync(string uri, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetAddressBookAsync(IEnumerable<uint> entires, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> AddAddressBookAsync(string address, string description = null, string payment_id = null, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> DeleteAddressBookAsync(uint index, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> RefreshWalletAsync(uint start_height, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> RescanSpentAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> OpenWalletAsync(string filename, string password = null, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> CloseWalletAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> ChangeWalletPasswordAsync(string old_password = null, string new_password = null, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> GetVersionAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> IsMultiSigAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> PrepareMultiSigAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> MakeMultiSigAsync(IEnumerable<string> multisig_info, uint threshold, string password, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> ExportMultiSigInfoAsync(CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> FinalizeMultiSigAsync(IEnumerable<string> multisig_info, string password, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SignMultiSigAsync(string tx_data_hex, CancellationToken token = default);
        Task<MoneroWalletDataRetrieverResponse> SubmitMultiSigAsync(string tx_data_hex, CancellationToken token = default);
    }
}
