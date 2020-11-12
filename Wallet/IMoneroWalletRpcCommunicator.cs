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
    internal interface IMoneroWalletRpcCommunicator : IDisposable
    {
        Task<MoneroWalletCommunicatorResponse> GetBalanceAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetBalanceAsync(uint account_index, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetAddressAsync(uint account_index, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetAddressAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetAddressIndexAsync(string address, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> CreateAddressAsync(uint account_index, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> CreateAddressAsync(uint account_index, string label, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> LabelAddressAsync(uint major_index, uint minor_index, string label, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetAccountsAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetAccountsAsync(string tag, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> CreateAccountAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> CreateAccountAsync(string label, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> LabelAccountAsync(uint account_index, string label, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetAccountTagsAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetHeightAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, uint unlock_time = 0, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool new_algorithm = true, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, bool new_algorithm = true, uint unlock_time = 0, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SignTransferAsync(string unsigned_txset, bool export_raw = false, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SubmitTransferAsync(string tx_data_hex, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SweepDustAsync(bool get_tx_keys, bool get_tx_hex, bool get_tx_metadata, bool do_not_relay = false, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SweepAllAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time = 0, ulong below_amount = ulong.MaxValue, bool get_tx_keys = true, bool get_tx_hex = true, bool get_tx_metadata = true, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SaveWalletAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, bool return_key_image = false, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, bool return_key_image = false, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, IEnumerable<uint> subaddr_indices, bool return_key_image = false, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetPrivateKey(KeyType key_type, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> StopWalletAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetTransactionKeyAsync(string txid, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> CheckTransactionKeyAsync(string txid, string tx_key, string address, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint min_height, uint max_height, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetTransferByTxidAsync(string txid, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetTransferByTxidAsync(string txid, uint account_index, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SignAsync(string data, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> VerifyAsync(string data, string address, string signature, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> ExportOutputsAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> ImportOutputsAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> ExportKeyImagesAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> ImportKeyImagesAsync(IEnumerable<(string key_image, string signature)> signed_key_images, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> MakeUriAsync(string address, ulong amount, string recipient_name, string tx_description = null, string payment_id = null, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> ParseUriAsync(string uri, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetAddressBookAsync(IEnumerable<uint> entires, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> AddAddressBookAsync(string address, string description = null, string payment_id = null, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> DeleteAddressBookAsync(uint index, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> RefreshWalletAsync(uint start_height, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> RescanSpentAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> OpenWalletAsync(string filename, string password = null, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> CloseWalletAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> ChangeWalletPasswordAsync(string old_password = null, string new_password = null, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> GetVersionAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> IsMultiSigAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> PrepareMultiSigAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> MakeMultiSigAsync(IEnumerable<string> multisig_info, uint threshold, string password, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> ExportMultiSigInfoAsync(CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> FinalizeMultiSigAsync(IEnumerable<string> multisig_info, string password, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SignMultiSigAsync(string tx_data_hex, CancellationToken token = default);
        Task<MoneroWalletCommunicatorResponse> SubmitMultiSigAsync(string tx_data_hex, CancellationToken token = default);
    }
}
