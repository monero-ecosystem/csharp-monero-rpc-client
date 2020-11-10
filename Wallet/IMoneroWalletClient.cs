using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MoneroClient.Wallet.POD.Requests;
using MoneroClient.Wallet.POD.Responses;
using MoneroClient.Wallet.POD;

namespace MoneroClient.Wallet
{
    interface IMoneroWalletClient : IDisposable
    {
        /// <summary>
        /// Returns wallet's balance.
        /// </summary>
        Task<MoneroWalletResponse> GetBalanceAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default);
        /// <summary>
        /// Returns wallet's balance.
        /// </summary>
        Task<MoneroWalletResponse> GetBalanceAsync(uint account_index, CancellationToken token = default);
        /// <summary>
        /// Return the wallet's addresses for an account.
        /// </summary>
        Task<MoneroWalletResponse> GetAddressAsync(uint account_index, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetAddressAsync(uint, CancellationToken)"/> Also filter for specific set of subaddresses.
        /// </summary>
        Task<MoneroWalletResponse> GetAddressAsync(uint account_index, IEnumerable<uint> address_indices, CancellationToken token = default);
        /// <summary>
        /// Get account and address indexes from a specific (sub)address
        /// </summary>
        Task<MoneroWalletResponse> GetAddressIndexAsync(string address, CancellationToken token = default);
        /// <summary>
        /// Create a new address for an account.
        /// </summary>
        Task<MoneroWalletResponse> CreateAddressAsync(uint account_index, CancellationToken token = default);
        /// <summary>
        ///  <seealso cref="CreateAccountAsync(CancellationToken)"/> Also label the new address.
        /// </summary>
        Task<MoneroWalletResponse> CreateAddressAsync(uint account_index, string label, CancellationToken token = default);
        /// <summary>
        /// Label an address.
        /// </summary>
        Task<MoneroWalletResponse> LabelAddressAsync(uint major_index, uint minor_index, string label, CancellationToken token = default);
        /// <summary>
        /// Get all accounts for a wallet.
        /// </summary>
        Task<MoneroWalletResponse> GetAccountsAsync(CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetAccountsAsync(CancellationToken)"/> Also filter accounts by tag.
        /// </summary>
        Task<MoneroWalletResponse> GetAccountsAsync(string tag, CancellationToken token = default);
        /// <summary>
        /// Create a new account.
        /// </summary>
        Task<MoneroWalletResponse> CreateAccountAsync(CancellationToken token = default);
        /// <summary>
        /// <seealso cref="CreateAccountAsync(CancellationToken)"/> Doing so with an optional label.
        /// </summary>
        Task<MoneroWalletResponse> CreateAccountAsync(string label, CancellationToken token = default);
        /// <summary>
        /// Label an account.
        /// </summary>
        Task<MoneroWalletResponse> LabelAccountAsync(uint account_index, string label, CancellationToken token = default);
        /// <summary>
        /// Get a list of user-defined account tags.
        /// </summary>
        Task<MoneroWalletResponse> GetAccountTagsAsync(CancellationToken token = default);
        /// <summary>
        /// Apply a filtering tag to a list of accounts.
        /// </summary>
        Task<MoneroWalletResponse> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default);
        /// <summary>
        /// Remove filtering tag from a list of accounts.
        /// </summary>
        Task<MoneroWalletResponse> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default);
        /// <summary>
        /// Set description for an account tag.
        /// </summary>
        Task<MoneroWalletResponse> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default);
        /// <summary>
        /// Returns the wallet's current block height.
        /// </summary>
        Task<MoneroWalletResponse> GetHeightAsync(CancellationToken token = default);
        /// <summary>
        /// Send monero to a number of recipients.
        /// </summary>
        Task<MoneroWalletResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, uint unlock_time = 0, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        /// <summary>
        /// Same as transfer, but can split into more than one tx if necessary.
        /// </summary>
        Task<MoneroWalletResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool new_algorithm = true, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, bool get_tx_key, bool get_tx_hex, bool new_algorithm = true, uint unlock_time = 0, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transfer_priority, uint ring_size, uint account_index, bool new_algorithm = true, uint unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, CancellationToken token = default);
        /// <summary>
        /// Sign a transaction created on a read-only wallet (in cold-signing process).
        /// </summary>
        Task<MoneroWalletResponse> SignTransferAsync(string unsigned_txset, bool export_raw = false, CancellationToken token = default);
        /// <summary>
        /// Submit a previously signed transaction on a read-only wallet (in cold-signing process).
        /// </summary>
        Task<MoneroWalletResponse> SubmitTransferAsync(string tx_data_hex, CancellationToken token = default);
        /// <summary>
        /// Send all dust outputs back to the wallet's, to make them easier to spend (and mix).
        /// </summary>
        Task<MoneroWalletResponse> SweepDustAsync(bool get_tx_keys, bool get_tx_hex, bool get_tx_metadata, bool do_not_relay = false, CancellationToken token = default);
        /// <summary>
        /// Send all unlocked balance to an address. Be careful...
        /// </summary>
        Task<MoneroWalletResponse> SweepAllAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time = 0, ulong below_amount = ulong.MaxValue, bool get_tx_keys = true, bool get_tx_hex = true, bool get_tx_metadata = true, CancellationToken token = default);
        /// <summary>
        /// Save the wallet.
        /// </summary>
        Task<MoneroWalletResponse> SaveWalletAsync(CancellationToken token = default);
        /// <summary>
        /// Return a list of incoming transfers to the wallet.
        /// </summary>
        Task<MoneroWalletResponse> GetIncomingTransfersAsync(TransferType transfer_type, bool return_key_image = false, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetIncomingTransfersAsync(TransferType, bool, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, bool return_key_image = false, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetIncomingTransfersAsync(TransferType, bool, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> GetIncomingTransfersAsync(TransferType transfer_type, uint account_index, IEnumerable<uint> subaddr_indices, bool return_key_image = false, CancellationToken token = default);
        /// <summary>
        /// Return the spend or view private key.
        /// </summary>
        Task<MoneroWalletResponse> GetPrivateKey(KeyType key_type, CancellationToken token = default);
        /// <summary>
        /// Stops the wallet, storing the current state.
        /// </summary>
        Task<MoneroWalletResponse> StopWalletAsync(CancellationToken token = default);
        /// <summary>
        /// Set arbitrary string notes for transaction.
        /// </summary>
        Task<MoneroWalletResponse> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default);
        /// <summary>
        /// Get string notes for transactions of interest.
        /// </summary>
        Task<MoneroWalletResponse> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default);
        /// <summary>
        /// Get transaction secret key from transaction id.
        /// </summary>
        Task<MoneroWalletResponse> GetTransactionKeyAsync(string txid, CancellationToken token = default);
        /// <summary>
        /// Check a transaction in the blockchain with its secret key.
        /// </summary>
        Task<MoneroWalletResponse> CheckTransactionKeyAsync(string txid, string tx_key, string address, CancellationToken token = default);
        /// <summary>
        /// Returns a list of transfers.
        /// </summary>
        /// <param name="in">Include incoming transfers.</param>
        /// <param name="out">Include outgoing transfers.</param>
        /// <param name="pending">Include pending transfers.</param>
        /// <param name="failed">Include failed transfers.</param>
        /// <param name="pool">Include transfers from the daemon's transaction pool.</param>
        Task<MoneroWalletResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetTransfersAsync(bool, bool, bool, bool, bool, CancellationToken)"/>
        /// </summary>
        /// <param name="min_height">Minimum block height to scan for transfers, if filtering by height is enabled.</param>
        /// <param name="max_height">Maximum block height to scan for transfers, if filtering by height is enabled.</param>
        /// <param name="filter_by_height">Filter transfers by block height.</param>
        Task<MoneroWalletResponse> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint min_height, uint max_height, CancellationToken token = default);
        /// <summary>
        /// Show information about a transfer to/from this address.
        /// </summary>
        Task<MoneroWalletResponse> GetTransferByTxidAsync(string txid, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetTransferByTxidAsync(string, CancellationToken)"/>
        /// </summary>
        Task<MoneroWalletResponse> GetTransferByTxidAsync(string txid, uint account_index, CancellationToken token = default);
        /// <summary>
        /// Sign a string.
        /// </summary>
        Task<MoneroWalletResponse> SignAsync(string data, CancellationToken token = default);
        /// <summary>
        /// Verify a signature on a string.
        /// </summary>
        Task<MoneroWalletResponse> VerifyAsync(string data, string address, string signature, CancellationToken token = default);
        /// <summary>
        /// Export outputs in hex format.
        /// </summary>
        Task<MoneroWalletResponse> ExportOutputsAsync(CancellationToken token = default);
        /// <summary>
        /// Import outputs in hex format.
        /// </summary>
        Task<MoneroWalletResponse> ImportOutputsAsync(CancellationToken token = default);
        /// <summary>
        /// Export a signed set of key images.
        /// </summary>
        Task<MoneroWalletResponse> ExportKeyImagesAsync(CancellationToken token = default);
        /// <summary>
        /// Import signed key images list and verify their spent status.
        /// </summary>
        Task<MoneroWalletResponse> ImportKeyImagesAsync(IEnumerable<(string key_image, string signature)> signed_key_images, CancellationToken token = default);
        /// <summary>
        /// Create a payment URI using the official URI spec.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <param name="amount">Amount to receive in atomicunits.</param>
        /// <param name="recipient_name">Name of the payment recipient.</param>
        /// <param name="tx_description">Description of the reason for the tx.</param>
        /// <param name="payment_id">16 or 64 character hexadecimal payment id.</param>
        Task<MoneroWalletResponse> MakeUriAsync(string address, ulong amount, string recipient_name, string tx_description = null, string payment_id = null, CancellationToken token = default);
        /// <summary>
        /// Parse a payment URI to get payment information.
        /// </summary>
        Task<MoneroWalletResponse> ParseUriAsync(string uri, CancellationToken token = default);
        /// <summary>
        /// Retrieves entries from the address book.
        /// </summary>
        /// <param name="entires">Indices of the requested address book entries.</param>
        Task<MoneroWalletResponse> GetAddressBookAsync(IEnumerable<uint> entires, CancellationToken token = default);
        /// <summary>
        /// Add an entry to the address book.
        /// </summary>
        Task<MoneroWalletResponse> AddAddressBookAsync(string address, string description = null, string payment_id = null, CancellationToken token = default);
        /// <summary>
        /// Delete an entry from ther address book.
        /// </summary>
        Task<MoneroWalletResponse> DeleteAddressBookAsync(uint index, CancellationToken token = default);
        /// <summary>
        /// Refresh a wallet after opening.
        /// </summary>
        Task<MoneroWalletResponse> RefreshWalletAsync(uint start_height, CancellationToken token = default);
        /// <summary>
        /// Rescan the blockchain for spent outputs.
        /// </summary>
        Task<MoneroWalletResponse> RescanSpentAsync(CancellationToken token = default);
        /// <summary>
        /// Create a new wallet. You need to have set the argument "–wallet-dir" when launching monero-wallet-rpc to make this work.
        /// </summary>
        /// <param name="filename">Wallet file name.</param>
        /// <param name="language">Language for your wallet's seed.</param>
        /// <param name="password">Password to protect the wallet.</param>
        Task<MoneroWalletResponse> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default);
        /// <summary>
        /// Open a wallet. You need to have set the argument "–wallet-dir" when launching monero-wallet-rpc to make this work.
        /// </summary>
        Task<MoneroWalletResponse> OpenWalletAsync(string filename, string password = null, CancellationToken token = default);
        /// <summary>
        /// Close the currently opened wallet, after trying to save it.
        /// </summary>
        Task<MoneroWalletResponse> CloseWalletAsync(CancellationToken token = default);
        /// <summary>
        /// Change a wallet password.
        /// </summary>
        Task<MoneroWalletResponse> ChangeWalletPasswordAsync(string old_password = null, string new_password = null, CancellationToken token = default);
        /// <summary>
        /// Get RPC version Major & Minor integer-format, where Major is the first 16 bits and Minor the last 16 bits.
        /// </summary>
        Task<MoneroWalletResponse> GetVersionAsync(CancellationToken token = default);
        /// <summary>
        /// Check if a wallet is a multi-signature (multisig) one.
        /// </summary>
        Task<MoneroWalletResponse> IsMultiSigAsync(CancellationToken token = default);
        /// <summary>
        /// Prepare a wallet for multisig by generating a multisig string to share with peers.
        /// </summary>
        Task<MoneroWalletResponse> PrepareMultiSigAsync(CancellationToken token = default);
        /// <summary>
        /// Make a wallet multisig by importing peers multisig string.
        /// </summary>
        /// <param name="multisig_info">List of multisig string from peers.</param>
        /// <param name="threshold">Amount of signatures needed to sign a transfer. Must be less or equal than the amount of signature in multisig_info.</param>
        /// <param name="password">Wallet password.</param>
        Task<MoneroWalletResponse> MakeMultiSigAsync(IEnumerable<string> multisig_info, uint threshold, string password, CancellationToken token = default);
        /// <summary>
        /// Export multisig info for other participants.
        /// </summary>
        Task<MoneroWalletResponse> ExportMultiSigInfoAsync(CancellationToken token = default);
        /// <summary>
        /// Import multisig info from other participants.
        /// </summary>
        /// <param name="info"> List of multisig info in hex format from other participants.</param>
        Task<MoneroWalletResponse> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default);
        /// <summary>
        /// Turn this wallet into a multisig wallet, extra step for N-1/N wallets.
        /// </summary>
        /// <param name="multisig_info">List of multisig string from peers.</param>
        /// <param name="password">Wallet password.</param>
        Task<MoneroWalletResponse> FinalizeMultiSigAsync(IEnumerable<string> multisig_info, string password, CancellationToken token = default);
        /// <summary>
        /// Sign a transaction in multisig.
        /// </summary>
        /// <param name="tx_data_hex">Multisig transaction in hex format, as returned by transfer under multisig_txset.</param>
        Task<MoneroWalletResponse> SignMultiSigAsync(string tx_data_hex, CancellationToken token = default);
        /// <summary>
        /// Submit a signed multisig transaction.
        /// </summary>
        /// <param name="tx_data_hex">Multisig transaction in hex format, as returned by sign_multisig under tx_data_hex.</param>
        Task<MoneroWalletResponse> SubmitMultiSigAsync(string tx_data_hex, CancellationToken token = default);
    }
}
