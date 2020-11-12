using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Wallet
{
    public interface IMoneroWalletClient : IDisposable
    {
        /// <summary>
        /// Returns wallet's balance.
        /// </summary>
        Task<Balance> GetBalanceAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default);
        /// <summary>
        /// Returns wallet's balance.
        /// </summary>
        Task<Balance> GetBalanceAsync(uint accountIndex, CancellationToken token = default);
        /// <summary>
        /// Return the wallet's addresses for an account.
        /// </summary>
        Task<AddressResult> GetAddressAsync(uint accountIndex, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetAddressAsync(uint, CancellationToken)"/> Also filter for specific set of subaddresses.
        /// </summary>
        Task<AddressResult> GetAddressAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default);
        /// <summary>
        /// Get account and address indexes from a specific (sub)address
        /// </summary>
        Task<AddressIndex> GetAddressIndexAsync(string address, CancellationToken token = default);
        /// <summary>
        /// Create a new address for an account.
        /// </summary>
        Task<AddressCreation> CreateAddressAsync(uint accountIndex, CancellationToken token = default);
        /// <summary>
        ///  <seealso cref="CreateAccountAsync(CancellationToken)"/> Also label the new address.
        /// </summary>
        Task<AddressCreation> CreateAddressAsync(uint accountIndex, string label, CancellationToken token = default);
        /// <summary>
        /// Label an address.
        /// </summary>
        Task<AddressLabel> LabelAddressAsync(uint majorIndex, uint minorIndex, string label, CancellationToken token = default);
        /// <summary>
        /// Get all accounts for a wallet.
        /// </summary>
        Task<AccountResult> GetAccountsAsync(CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetAccountsAsync(CancellationToken)"/> Also filter accounts by tag.
        /// </summary>
        Task<AccountResult> GetAccountsAsync(string tag, CancellationToken token = default);
        /// <summary>
        /// Create a new account.
        /// </summary>
        Task<CreateAccount> CreateAccountAsync(CancellationToken token = default);
        /// <summary>
        /// <seealso cref="CreateAccountAsync(CancellationToken)"/> Doing so with an optional label.
        /// </summary>
        Task<CreateAccount> CreateAccountAsync(string label, CancellationToken token = default);
        /// <summary>
        /// Label an account.
        /// </summary>
        Task<AccountLabel> LabelAccountAsync(uint account_index, string label, CancellationToken token = default);
        /// <summary>
        /// Get a list of user-defined account tags.
        /// </summary>
        Task<AccountTags> GetAccountTagsAsync(CancellationToken token = default);
        /// <summary>
        /// Apply a filtering tag to a list of accounts.
        /// </summary>
        Task<TagAccounts> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default);
        /// <summary>
        /// Remove filtering tag from a list of accounts.
        /// </summary>
        Task<UntagAccounts> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default);
        /// <summary>
        /// Set description for an account tag.
        /// </summary>
        Task<AccountTagAndDescription> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default);
        /// <summary>
        /// Returns the wallet's current block height.
        /// </summary>
        Task<BlockchainHeight> GetHeightAsync(CancellationToken token = default);
        /// <summary>
        /// Send monero to a number of recipients.
        /// </summary>
        Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, uint unlockTime = 0, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default);
        /// <summary>
        /// Same as transfer, but can split into more than one tx if necessary.
        /// </summary>
        Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool newAlgorithm = true, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>
        /// </summary>
        Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, bool newAlgorithm = true, uint unlockTime = 0, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>
        /// </summary>
        Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, bool newAlgorithm = true, uint unlockTime = 0, bool get_tx_key = true, bool getTxHex = true, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default);
        /// <summary>
        /// Sign a transaction created on a read-only wallet (in cold-signing process).
        /// </summary>
        Task<SignTransferResult> SignTransferAsync(string unsignedTxSet, bool exportRaw = false, CancellationToken token = default);
        /// <summary>
        /// Submit a previously signed transaction on a read-only wallet (in cold-signing process).
        /// </summary>
        Task<SubmitTransfer> SubmitTransferAsync(string txDataHex, CancellationToken token = default);
        /// <summary>
        /// Send all dust outputs back to the wallet's, to make them easier to spend (and mix).
        /// </summary>
        Task<SweepDust> SweepDustAsync(bool getTxKey, bool getTxHex, bool getTxMetadata, bool doNotRelay = false, CancellationToken token = default);
        /// <summary>
        /// Send all unlocked balance to an address. Be careful...
        /// </summary>
        Task<SplitFundTransfer> SweepAllAsync(string address, uint accountIndex, TransferPriority transactionPriority, uint ringSize, ulong unlockTime = 0, ulong belowAmount = ulong.MaxValue, bool getTxKeys = true, bool getTxHex = true, bool getTxMetadata = true, CancellationToken token = default);
        /// <summary>
        /// Save the wallet.
        /// </summary>
        Task<SaveWallet> SaveWalletAsync(CancellationToken token = default);
        /// <summary>
        /// Return a list of incoming transfers to the wallet.
        /// </summary>
        Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, bool returnKeyImage = false, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetIncomingTransfersAsync(TransferType, bool, CancellationToken)"/>
        /// </summary>
        Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, bool returnKeyImage = false, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetIncomingTransfersAsync(TransferType, bool, CancellationToken)"/>
        /// </summary>
        Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, IEnumerable<uint> subaddrIndices, bool returnKeyImage = false, CancellationToken token = default);
        /// <summary>
        /// Return the spend or view private key.
        /// </summary>
        Task<QueryKey> GetPrivateKey(KeyType keyType, CancellationToken token = default);
        /// <summary>
        /// Stops the wallet, storing the current state.
        /// </summary>
        Task<StopWalletResult> StopWalletAsync(CancellationToken token = default);
        /// <summary>
        /// Set arbitrary string notes for transaction.
        /// </summary>
        Task<SetTransactionNotes> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default);
        /// <summary>
        /// Get string notes for transactions of interest.
        /// </summary>
        Task<GetTransactionNotes> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default);
        /// <summary>
        /// Get transaction secret key from transaction id.
        /// </summary>
        Task<GetTransactionKey> GetTransactionKeyAsync(string txid, CancellationToken token = default);
        /// <summary>
        /// Check a transaction in the blockchain with its secret key.
        /// </summary>
        Task<CheckTransactionKey> CheckTransactionKeyAsync(string txid, string txKey, string address, CancellationToken token = default);
        /// <summary>
        /// Returns a list of transfers.
        /// </summary>
        /// <param name="in">Include incoming transfers.</param>
        /// <param name="out">Include outgoing transfers.</param>
        /// <param name="pending">Include pending transfers.</param>
        /// <param name="failed">Include failed transfers.</param>
        /// <param name="pool">Include transfers from the daemon's transaction pool.</param>
        Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetTransfersAsync(bool, bool, bool, bool, bool, CancellationToken)"/>
        /// </summary>
        /// <param name="minHeight">Minimum block height to scan for transfers, if filtering by height is enabled.</param>
        /// <param name="maxHeight">Maximum block height to scan for transfers, if filtering by height is enabled.</param>
        Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint minHeight, uint maxHeight, CancellationToken token = default);
        /// <summary>
        /// Show information about a transfer to/from this address.
        /// </summary>
        Task<ShowTransferByTxid> GetTransferByTxidAsync(string txid, CancellationToken token = default);
        /// <summary>
        /// <seealso cref="GetTransferByTxidAsync(string, CancellationToken)"/>
        /// </summary>
        Task<ShowTransferByTxid> GetTransferByTxidAsync(string txid, uint accountIndex, CancellationToken token = default);
        /// <summary>
        /// Sign a string.
        /// </summary>
        Task<Signature> SignAsync(string data, CancellationToken token = default);
        /// <summary>
        /// Verify a signature on a string.
        /// </summary>
        Task<VerifyResult> VerifyAsync(string data, string address, string signature, CancellationToken token = default);
        /// <summary>
        /// Export outputs in hex format.
        /// </summary>
        Task<ExportOutputs> ExportOutputsAsync(CancellationToken token = default);
        /// <summary>
        /// Import outputs in hex format.
        /// </summary>
        Task<ImportOutputsResult> ImportOutputsAsync(CancellationToken token = default);
        /// <summary>
        /// Export a signed set of key images.
        /// </summary>
        Task<ExportKeyImages> ExportKeyImagesAsync(CancellationToken token = default);
        /// <summary>
        /// Import signed key images list and verify their spent status.
        /// </summary>
        Task<ImportKeyImages> ImportKeyImagesAsync(IEnumerable<(string keyImage, string signature)> signedKeyImages, CancellationToken token = default);
        /// <summary>
        /// Create a payment URI using the official URI spec.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <param name="amount">Amount to receive in atomicunits.</param>
        /// <param name="recipientName">Name of the payment recipient.</param>
        /// <param name="txDescription">Description of the reason for the tx.</param>
        /// <param name="paymentId">16 or 64 character hexadecimal payment id.</param>
        Task<MakeUri> MakeUriAsync(string address, ulong amount, string recipientName, string txDescription = null, string paymentId = null, CancellationToken token = default);
        /// <summary>
        /// Parse a payment URI to get payment information.
        /// </summary>
        Task<ParseUri> ParseUriAsync(string uri, CancellationToken token = default);
        /// <summary>
        /// Retrieves entries from the address book.
        /// </summary>
        /// <param name="entires">Indices of the requested address book entries.</param>
        Task<GetAddressBook> GetAddressBookAsync(IEnumerable<uint> entires, CancellationToken token = default);
        /// <summary>
        /// Add an entry to the address book.
        /// </summary>
        Task<AddAddressBook> AddAddressBookAsync(string address, string description = null, string paymentId = null, CancellationToken token = default);
        /// <summary>
        /// Delete an entry from ther address book.
        /// </summary>
        Task<DeleteAddressBook> DeleteAddressBookAsync(uint index, CancellationToken token = default);
        /// <summary>
        /// Refresh a wallet after opening.
        /// </summary>
        Task<RefreshWallet> RefreshWalletAsync(uint startHeight, CancellationToken token = default);
        /// <summary>
        /// Rescan the blockchain for spent outputs.
        /// </summary>
        Task<RescanSpent> RescanSpentAsync(CancellationToken token = default);
        /// <summary>
        /// Create a new wallet. You need to have set the argument "–wallet-dir" when launching monero-wallet-rpc to make this work.
        /// </summary>
        /// <param name="filename">Wallet file name.</param>
        /// <param name="language">Language for your wallet's seed.</param>
        /// <param name="password">Password to protect the wallet.</param>
        Task<CreateWallet> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default);
        /// <summary>
        /// Open a wallet. You need to have set the argument "–wallet-dir" when launching monero-wallet-rpc to make this work.
        /// </summary>
        Task<OpenWallet> OpenWalletAsync(string filename, string password = null, CancellationToken token = default);
        /// <summary>
        /// Close the currently opened wallet, after trying to save it.
        /// </summary>
        Task<CloseWallet> CloseWalletAsync(CancellationToken token = default);
        /// <summary>
        /// Change a wallet password.
        /// </summary>
        Task<ChangeWalletPassword> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default);
        /// <summary>
        /// Get RPC version Major & Minor integer-format, where Major is the first 16 bits and Minor the last 16 bits.
        /// </summary>
        Task<GetVersion> GetVersionAsync(CancellationToken token = default);
        /// <summary>
        /// Check if a wallet is a multi-signature (multisig) one.
        /// </summary>
        Task<IsMultiSigInformation> IsMultiSigAsync(CancellationToken token = default);
        /// <summary>
        /// Prepare a wallet for multisig by generating a multisig string to share with peers.
        /// </summary>
        Task<PrepareMultiSig> PrepareMultiSigAsync(CancellationToken token = default);
        /// <summary>
        /// Make a wallet multisig by importing peers multisig string.
        /// </summary>
        /// <param name="multiSigInfo">List of multisig string from peers.</param>
        /// <param name="threshold">Amount of signatures needed to sign a transfer. Must be less or equal than the amount of signature in multisig_info.</param>
        /// <param name="password">Wallet password.</param>
        Task<MakeMultiSig> MakeMultiSigAsync(IEnumerable<string> multiSigInfo, uint threshold, string password, CancellationToken token = default);
        /// <summary>
        /// Export multisig info for other participants.
        /// </summary>
        Task<ExportMultiSigInformation> ExportMultiSigInfoAsync(CancellationToken token = default);
        /// <summary>
        /// Import multisig info from other participants.
        /// </summary>
        /// <param name="info"> List of multisig info in hex format from other participants.</param>
        Task<ImportMultiSigInformation> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default);
        /// <summary>
        /// Turn this wallet into a multisig wallet, extra step for N-1/N wallets.
        /// </summary>
        /// <param name="multisigInfo">List of multisig string from peers.</param>
        /// <param name="password">Wallet password.</param>
        Task<FinalizeMultiSig> FinalizeMultiSigAsync(IEnumerable<string> multisigInfo, string password, CancellationToken token = default);
        /// <summary>
        /// Sign a transaction in multisig.
        /// </summary>
        /// <param name="txDataHex">Multisig transaction in hex format, as returned by transfer under multisig_txset.</param>
        Task<SignMultiSigTransactionResult> SignMultiSigAsync(string txDataHex, CancellationToken token = default);
        /// <summary>
        /// Submit a signed multisig transaction.
        /// </summary>
        /// <param name="txDataHex">Multisig transaction in hex format, as returned by sign_multisig under tx_data_hex.</param>
        Task<SubmitMultiSig> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default);
    }
}
