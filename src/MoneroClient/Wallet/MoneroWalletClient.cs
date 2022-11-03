using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Monero.Client.Enums;
using Monero.Client.Network;
using Monero.Client.Utilities;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Responses;

namespace Monero.Client.Wallet
{
    public class MoneroWalletClient : IMoneroWalletClient
    {
        private readonly RpcCommunicator moneroRpcCommunicator;
        private readonly object disposingLock = new object();
        private bool disposed = false;

        private MoneroWalletClient(string host, uint port)
        {
            this.moneroRpcCommunicator = new RpcCommunicator(host, port);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoneroWalletClient"/> class using default network settings (<localhost>:<defaultport>).
        /// </summary>
        private MoneroWalletClient(MoneroNetwork networkType)
        {
            this.moneroRpcCommunicator = new RpcCommunicator(networkType, ConnectionType.Wallet);
        }

        /// <summary>
        /// Initialize a Monero Wallet Client using default network settings (<localhost>:<defaultport>), opening the wallet while doing so.
        /// </summary>
        public static Task<MoneroWalletClient> CreateAsync(string host, uint port, string filename, string password, CancellationToken cancellationToken = default)
        {
            var moneroWalletClient = new MoneroWalletClient(host, port);
            return moneroWalletClient.InitializeAsync(filename, password, cancellationToken);
        }

        public static Task<MoneroWalletClient> CreateAsync(MoneroNetwork networkType, string filename, string password, CancellationToken cancellationToken = default)
        {
            var moneroWalletClient = new MoneroWalletClient(networkType);
            return moneroWalletClient.InitializeAsync(filename, password, cancellationToken);
        }

        public static Task<CreateWallet> CreateNewWalletAsync(string host, uint port, string filename, string password, string language, CancellationToken cancellationToken = default)
        {
            using var moneroWalletClient = new MoneroWalletClient(host, port);
            return moneroWalletClient.CreateWalletAsync(filename, language, password, cancellationToken);
        }

        /// <summary>
        /// Disposes the object (also calls <see cref="CloseWalletAsync(CancellationToken)"/>).
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Returns wallet's balance.
        /// </summary>
        public async Task<Balance> GetBalanceAsync(uint accountIndex, IEnumerable<uint> addressIndices, bool allAccounts = false, bool strict = false, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBalanceAsync(accountIndex, addressIndices, allAccounts, strict, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BalanceResponse, nameof(this.GetBalanceAsync));
            return result.BalanceResponse.Result;
        }

        /// <summary>
        /// Returns wallet's balance for a given account index.
        /// </summary>
        public async Task<Balance> GetBalanceAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetBalanceAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BalanceResponse, nameof(this.GetBalanceAsync));
            return result.BalanceResponse.Result;
        }

        /// <summary>
        /// Return the wallet's addresses for an account.
        /// </summary>
        public async Task<Addresses> GetAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAddressAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressResponse, nameof(this.GetAddressAsync));
            return result.AddressResponse.Result;
        }

        /// <summary>
        /// <seealso cref="GetAddressAsync(uint, CancellationToken)"/> Also filter for specific set of subaddresses.
        /// </summary>
        public async Task<Addresses> GetAddressAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAddressAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressResponse, nameof(this.GetAddressAsync));
            return result.AddressResponse.Result;
        }

        /// <summary>
        /// Get account and address indexes from a specific (sub)address
        /// </summary>
        public async Task<AddressIndex> GetAddressIndexAsync(string address, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAddressIndexAsync(address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressIndexResponse, nameof(this.GetAddressIndexAsync));
            return result.AddressIndexResponse.Result;
        }

        /// <summary>
        /// Create a new address for an account.
        /// </summary>
        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.CreateAddressAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressCreationResponse, nameof(this.CreateAddressAsync));
            return result.AddressCreationResponse.Result;
        }

        /// <summary>
        ///  <seealso cref="CreateAccountAsync(CancellationToken)"/> Also label the new address.
        /// </summary>
        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.CreateAddressAsync(accountIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressCreationResponse, nameof(this.CreateAddressAsync));
            return result.AddressCreationResponse.Result;
        }

        /// <summary>
        /// Label an address.
        /// </summary>
        public async Task<AddressLabel> LabelAddressAsync(uint majorIndex, uint minorIndex, string label, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.LabelAddressAsync(majorIndex, minorIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressLabelResponse, nameof(this.LabelAddressAsync));
            return result.AddressLabelResponse.Result;
        }

        /// <summary>
        /// Get all accounts for a wallet.
        /// </summary>
        public async Task<Account> GetAccountsAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAccountsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountResponse, nameof(this.GetAccountsAsync));
            return result.AccountResponse.Result;
        }

        /// <summary>
        /// <seealso cref="GetAccountsAsync(CancellationToken)"/> Also filter accounts by tag.
        /// </summary>
        public async Task<Account> GetAccountsAsync(string tag, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAccountsAsync(tag, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountResponse, nameof(this.GetAccountsAsync));
            return result.AccountResponse.Result;
        }

        /// <summary>
        /// Create a new account.
        /// </summary>
        public async Task<CreateAccount> CreateAccountAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.CreateAccountAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateAccountResponse, nameof(this.CreateAccountAsync));
            return result.CreateAccountResponse.Result;
        }

        /// <summary>
        /// <seealso cref="CreateAccountAsync(CancellationToken)"/> Doing so with an optional label.
        /// </summary>
        public async Task<CreateAccount> CreateAccountAsync(string label, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.CreateAccountAsync(label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateAccountResponse, nameof(this.CreateAccountAsync));
            return result.CreateAccountResponse.Result;
        }

        /// <summary>
        /// Label an account.
        /// </summary>
        public async Task<AccountLabel> LabelAccountAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.LabelAccountAsync(accountIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountLabelResponse, nameof(this.LabelAccountAsync));
            return result.AccountLabelResponse.Result;
        }

        /// <summary>
        /// Get a list of user-defined account tags.
        /// </summary>
        public async Task<AccountTags> GetAccountTagsAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAccountTagsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountTagsResponse, nameof(this.GetAccountTagsAsync));
            return result.AccountTagsResponse.Result;
        }

        /// <summary>
        /// Apply a filtering tag to a list of accounts.
        /// </summary>
        public async Task<TagAccounts> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.TagAccountsAsync(tag, accounts, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TagAccountsResponse, nameof(this.TagAccountsAsync));
            return result.TagAccountsResponse.Result;
        }

        /// <summary>
        /// Remove filtering tag from a list of accounts.
        /// </summary>
        public async Task<UntagAccounts> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.UntagAccountsAsync(accounts, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.UntagAccountsResponse, nameof(this.UntagAccountsAsync));
            return result.UntagAccountsResponse.Result;
        }

        /// <summary>
        /// Set description for an account tag.
        /// </summary>
        public async Task<AccountTagAndDescription> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SetAccountTagDescriptionAsync(tag, description, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetAccountTagAndDescriptionResponse, nameof(this.SetAccountTagDescriptionAsync));
            return result.SetAccountTagAndDescriptionResponse.Result;
        }

        /// <summary>
        /// Returns the wallet's current block height.
        /// </summary>
        public async Task<ulong> GetHeightAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetHeightAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockchainHeightResponse, nameof(this.GetHeightAsync));
            return result.BlockchainHeightResponse.Result.Height;
        }

        /// <summary>
        /// Send monero to a number of recipients.
        /// </summary>
        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.TransferAsync(transactions, transferPriority, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(this.TransferAsync));
            return result.FundTransferResponse.Result;
        }

        /// <summary>
        /// <seealso cref="IMoneroWalletClient.TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, ulong unlockTime = 0, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfUnlockTimeIsToolarge(unlockTime, nameof(unlockTime));
            var result = await this.moneroRpcCommunicator.TransferAsync(transactions, transferPriority, getTxKey, getTxHex, unlockTime, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(this.TransferAsync));
            return result.FundTransferResponse.Result;
        }

        /// <summary>
        /// <seealso cref="IMoneroWalletClient.TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, ulong unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfUnlockTimeIsToolarge(unlockTime, nameof(unlockTime));
            var result = await this.moneroRpcCommunicator.TransferAsync(transactions, transferPriority, ringSize, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(this.TransferAsync));
            return result.FundTransferResponse.Result;
        }

        /// <summary>
        /// <seealso cref="IMoneroWalletClient.TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, ulong unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfUnlockTimeIsToolarge(unlockTime, nameof(unlockTime));
            var result = await this.moneroRpcCommunicator.TransferAsync(transactions, transferPriority, ringSize, accountIndex, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(this.TransferAsync));
            return result.FundTransferResponse.Result;
        }

        /// <summary>
        /// Same as transfer, but can split into more than one tx if necessary.
        /// </summary>
        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool newAlgorithm = true, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, newAlgorithm, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(this.TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        /// <summary>
        /// <see cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>.
        /// </summary>
        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, bool newAlgorithm = true, ulong unlockTime = 0, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfUnlockTimeIsToolarge(unlockTime, nameof(unlockTime));
            var result = await this.moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, getTxKey, getTxHex, newAlgorithm, unlockTime, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(this.TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        /// <summary>
        /// <see cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>.
        /// </summary>
        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, bool newAlgorithm = true, ulong unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfUnlockTimeIsToolarge(unlockTime, nameof(unlockTime));
            var result = await this.moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, ringSize, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(this.TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        /// <summary>
        /// <see cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>.
        /// </summary>
        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, bool newAlgorithm = true, ulong unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfUnlockTimeIsToolarge(unlockTime, nameof(unlockTime));
            var result = await this.moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, ringSize, accountIndex, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(this.TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        /// <summary>
        /// Sign a transaction created on a read-only wallet (in cold-signing process).
        /// </summary>
        public async Task<SignTransfer> SignTransferAsync(string unsignedTxSet, bool exportRaw = false, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SignTransferAsync(unsignedTxSet, exportRaw, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignTransferResponse, nameof(this.SignTransferAsync));
            return result.SignTransferResponse.Result;
        }

        /// <summary>
        /// Submit a previously signed transaction on a read-only wallet (in cold-signing process).
        /// </summary>
        /// <returns>A list of transaction hashes.</returns>
        public async Task<List<string>> SubmitTransferAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SubmitTransferAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitTransferResponse, nameof(this.SubmitTransferAsync));
            return result.SubmitTransferResponse.Result.TransactionHashes;
        }

        /// <summary>
        /// Send all dust outputs back to the wallet's, to make them easier to spend (and mix).
        /// </summary>
        public async Task<SweepDust> SweepDustAsync(bool getTxKey, bool getTxHex, bool getTxMetadata, bool doNotRelay = false, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SweepDustAsync(getTxKey, getTxHex, getTxMetadata, doNotRelay, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepDustResponse, nameof(this.SweepDustAsync));
            return result.SweepDustResponse.Result;
        }

        /// <summary>
        /// Send all unlocked balance to an address. Be careful...
        /// </summary>
        public async Task<SplitFundTransfer> SweepAllAsync(string address, uint accountIndex, TransferPriority transactionPriority, uint ringSize, ulong unlockTime = 0, ulong belowAmount = ulong.MaxValue, bool getTxKeys = true, bool getTxHex = true, bool getTxMetadata = true, CancellationToken token = default)
        {
            ErrorGuard.ThrowIfUnlockTimeIsToolarge(unlockTime, nameof(unlockTime));
            var result = await this.moneroRpcCommunicator.SweepAllAsync(address, accountIndex, transactionPriority, ringSize, unlockTime, belowAmount, getTxKeys, getTxHex, getTxMetadata, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepAllResponse, nameof(this.SweepAllAsync));
            return result.SweepAllResponse.Result;
        }

        /// <summary>
        /// Save the wallet.
        /// </summary>
        public async Task<SaveWallet> SaveWalletAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SaveWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SaveWalletResponse, nameof(this.SaveWalletAsync));
            return result.SaveWalletResponse.Result;
        }

        /// <summary>
        /// Return a list of incoming transfers to the wallet.
        /// </summary>
        public async Task<List<IncomingTransfer>> GetIncomingTransfersAsync(TransferType transferType, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(this.GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result.Transfers;
        }

        /// <summary>
        /// <seealso cref="GetIncomingTransfersAsync(TransferType, bool, CancellationToken)"/>
        /// </summary>
        public async Task<List<IncomingTransfer>> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, accountIndex, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(this.GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result.Transfers;
        }

        /// <summary>
        /// <seealso cref="GetIncomingTransfersAsync(TransferType, bool, CancellationToken)"/>
        /// </summary>
        public async Task<List<IncomingTransfer>> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, IEnumerable<uint> subaddrIndices, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, accountIndex, subaddrIndices, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(this.GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result.Transfers;
        }

        /// <summary>
        /// Return the spend or view private key.
        /// </summary>
        public async Task<string> GetPrivateKey(KeyType keyType, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetPrivateKey(keyType, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.QueryKeyResponse, nameof(this.GetPrivateKey));
            return result.QueryKeyResponse.Result.Key;
        }

        /// <summary>
        /// Stops the wallet, storing the current state.
        /// </summary>
        public async Task<StopWalletResult> StopWalletAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.StopWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.StopWalletResponse, nameof(this.StopWalletAsync));
            return result.StopWalletResponse.Result;
        }

        /// <summary>
        /// Set arbitrary string notes for transaction.
        /// </summary>
        public async Task<SetTransactionNotes> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SetTransactionNotesAsync(txids, notes, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetTransactionNotesResponse, nameof(this.SetTransactionNotesAsync));
            return result.SetTransactionNotesResponse.Result;
        }

        /// <summary>
        /// Get string notes for transactions of interest.
        /// </summary>
        public async Task<List<string>> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransactionNotesAsync(txids, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetTransactionNotesResponse, nameof(this.GetTransactionNotesAsync));
            return result.GetTransactionNotesResponse.Result.Notes;
        }

        /// <summary>
        /// Get transaction secret key from transaction id.
        /// </summary>
        public async Task<string> GetTransactionKeyAsync(string txid, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransactionKeyAsync(txid, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetTransactionKeyResponse, nameof(this.GetTransactionKeyAsync));
            return result.GetTransactionKeyResponse.Result.TransactionKey;
        }

        /// <summary>
        /// Check a transaction in the blockchain with its secret key.
        /// </summary>
        public async Task<CheckTransactionKey> CheckTransactionKeyAsync(string txid, string txKey, string address, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.CheckTransactionKeyAsync(txid, txKey, address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CheckTransactionKeyResponse, nameof(this.CheckTransactionKeyAsync));
            return result.CheckTransactionKeyResponse.Result;
        }

        /// <summary>
        /// Returns a list of transfers from one or more of the desired categories.
        /// </summary>
        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransfersAsync(@in, @out, pending, failed, pool, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ShowTransfersResponse, nameof(this.GetTransfersAsync));
            return result.ShowTransfersResponse.Result;
        }

        /// <summary>
        /// <seealso cref="GetTransfersAsync(bool, bool, bool, bool, bool, CancellationToken)"/>
        /// </summary>
        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, ulong minHeight, ulong maxHeight, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransfersAsync(@in, @out, pending, failed, pool, minHeight, maxHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ShowTransfersResponse, nameof(this.GetTransfersAsync));
            return result.ShowTransfersResponse.Result;
        }

        /// <summary>
        /// <seealso cref="GetTransferByTxidAsync(string, uint, CancellationToken)"/>
        /// </summary>
        public async Task<Transfer> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransferByTxidAsync(txid, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransferByTxidResponse, nameof(this.GetTransferByTxidAsync));
            return result.TransferByTxidResponse.Result.Transfer;
        }

        /// <summary>
        /// Show information about a transfer to/from this address.
        /// </summary>
        public async Task<Transfer> GetTransferByTxidAsync(string txid, uint accountIndex, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetTransferByTxidAsync(txid, accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransferByTxidResponse, nameof(this.GetTransferByTxidAsync));
            return result.TransferByTxidResponse.Result.Transfer;
        }

        /// <summary>
        /// Sign a string.
        /// </summary>
        public async Task<string> SignAsync(string data, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SignAsync(data, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignResponse, nameof(this.SignAsync));
            return result.SignResponse.Result.Sig;
        }

        /// <summary>
        /// Export outputs in hex format.
        /// </summary>
        /// <returns>Output data hex.</returns>
        public async Task<string> ExportOutputsAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ExportOutputsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportOutputsResponse, nameof(this.ExportOutputsAsync));
            return result.ExportOutputsResponse.Result.OutputsDataHex;
        }

        /// <summary>
        /// Import outputs in hex format.
        /// </summary>
        /// <returns>The number of outputs imported.</returns>
        public async Task<ulong> ImportOutputsAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ImportOutputsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportOutputsResponse, nameof(this.ImportOutputsAsync));
            return result.ImportOutputsResponse.Result.NumImported;
        }

        /// <summary>
        /// Export a signed set of key images.
        /// </summary>
        public async Task<List<SignedKeyImage>> ExportKeyImagesAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ExportKeyImagesAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportKeyImagesResponse, nameof(this.ExportKeyImagesAsync));
            return result.ExportKeyImagesResponse.Result.SignedKeyImages;
        }

        /// <summary>
        /// Import signed key images list and verify their spent status.
        /// </summary>
        public async Task<ImportKeyImages> ImportKeyImagesAsync(IEnumerable<(string keyImage, string signature)> signedKeyImages, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ImportKeyImagesAsync(signedKeyImages, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportKeyImagesResponse, nameof(this.ImportKeyImagesAsync));
            return result.ImportKeyImagesResponse.Result;
        }

        /// <summary>
        /// Create a payment URI using the official URI spec.
        /// </summary>
        /// <param name="address">Wallet address.</param>
        /// <param name="amount">Amount to receive in atomicunits.</param>
        /// <param name="recipientName">Name of the payment recipient.</param>
        /// <param name="txDescription">Description of the reason for the tx.</param>
        /// <param name="paymentId">16 or 64 character hexadecimal payment id.</param>
        /// <returns>Payment URI.</returns>
        public async Task<string> MakeUriAsync(string address, ulong amount, string recipientName, string txDescription = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.MakeUriAsync(address, amount, recipientName, txDescription, paymentId, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MakeUriResponse, nameof(this.MakeUriAsync));
            return result.MakeUriResponse.Result.Uri;
        }

        /// <summary>
        /// Parse a payment URI to get payment information.
        /// </summary>
        public async Task<MoneroUri> ParseUriAsync(string uri, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ParseUriAsync(uri, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ParseUriResponse, nameof(this.ParseUriAsync));
            return result.ParseUriResponse.Result.Uri;
        }

        /// <summary>
        /// Retrieves entries from the address book.
        /// </summary>
        /// <param name="entries">Indices of the requested address book entries.</param>
        public async Task<AddressBook> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAddressBookAsync(entries, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetAddressBookResponse, nameof(this.GetAddressBookAsync));
            return result.GetAddressBookResponse.Result;
        }

        /// <summary>
        /// Add an entry to the address book.
        /// </summary>
        public async Task<AddAddressBook> AddAddressBookAsync(string address, string description = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.AddAddressBookAsync(address, description, paymentId, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddAddressBookResponse, nameof(this.AddAddressBookAsync));
            return result.AddAddressBookResponse.Result;
        }

        /// <summary>
        /// Delete an entry from ther address book.
        /// </summary>
        public async Task<DeleteAddressBook> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.DeleteAddressBookAsync(index, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DeleteAddressBookResponse, nameof(this.DeleteAddressBookAsync));
            return result.DeleteAddressBookResponse.Result;
        }

        /// <summary>
        /// Refresh a wallet after opening.
        /// </summary>
        public async Task<RefreshWallet> RefreshWalletAsync(uint startHeight, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.RefreshWalletAsync(startHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RefreshWalletResponse, nameof(this.RefreshWalletAsync));
            return result.RefreshWalletResponse.Result;
        }

        /// <summary>
        /// Rescan the blockchain for spent outputs.
        /// </summary>
        public async Task<RescanSpent> RescanSpentAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.RescanSpentAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RescanSpentResponse, nameof(this.RescanSpentAsync));
            return result.RescanSpentResponse.Result;
        }

        /// <summary>
        /// Create a new wallet. You need to have set the argument "–wallet-dir" when launching monero-wallet-rpc to make this work.
        /// </summary>
        /// <param name="filename">Wallet file name.</param>
        /// <param name="language">Language for your wallet's seed.</param>
        /// <param name="password">Password to protect the wallet.</param>
        public async Task<CreateWallet> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.CreateWalletAsync(filename, language, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateWalletResponse, nameof(this.CreateWalletAsync));
            return result.CreateWalletResponse.Result;
        }

        /// <summary>
        /// Open a wallet. You need to have set the argument "–wallet-dir" when launching monero-wallet-rpc to make this work.
        /// </summary>
        public async Task<OpenWallet> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.OpenWalletAsync(filename, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.OpenWalletResponse, nameof(this.OpenWalletAsync));
            return result.OpenWalletResponse.Result;
        }

        /// <summary>
        /// Close the currently opened wallet, after trying to save it.
        /// Note: After you close a wallet, every subsequent query will fail.
        /// </summary>
        public async Task<CloseWallet> CloseWalletAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.CloseWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CloseWalletResponse, nameof(this.CloseWalletAsync));
            return result.CloseWalletResponse.Result;
        }

        /// <summary>
        /// Change a wallet password.
        /// </summary>
        public async Task<ChangeWalletPassword> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ChangeWalletPasswordAsync(oldPassword, newPassword, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ChangeWalletPasswordResponse, nameof(this.ChangeWalletPasswordAsync));
            return result.ChangeWalletPasswordResponse.Result;
        }

        /// <summary>
        /// Get RPC version Major & Minor integer-format, where Major is the first 16 bits and Minor the last 16 bits.
        /// </summary>
        public async Task<uint> GetVersionAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetRpcVersionAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetRpcVersionResponse, nameof(this.GetVersionAsync));
            return result.GetRpcVersionResponse.Result.Version;
        }

        /// <summary>
        /// Check if a wallet is a multi-signature (multisig) one.
        /// </summary>
        public async Task<MultiSigInformation> GetMultiSigInformationAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.IsMultiSigAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MultiSigInformationResponse, nameof(this.GetMultiSigInformationAsync));
            return result.MultiSigInformationResponse.Result;
        }

        /// <summary>
        /// Prepare a wallet for multisig by generating a multisig string to share with peers.
        /// </summary>
        /// <returns>A string representing multisig information.</returns>
        public async Task<string> PrepareMultiSigAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.PrepareMultiSigAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.PrepareMultiSigResponse, nameof(this.PrepareMultiSigAsync));
            return result.PrepareMultiSigResponse.Result.MultiSigInformation;
        }

        /// <summary>
        /// Make a wallet multisig by importing peers multisig string.
        /// </summary>
        /// <param name="multiSigInfo">List of multisig string from peers.</param>
        /// <param name="threshold">Amount of signatures needed to sign a transfer. Must be less or equal than the amount of signature in multisig_info.</param>
        /// <param name="password">Wallet password.</param>
        public async Task<MakeMultiSig> MakeMultiSigAsync(IEnumerable<string> multiSigInfo, uint threshold, string password, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.MakeMultiSigAsync(multiSigInfo, threshold, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MakeMultiSigResponse, nameof(this.MakeMultiSigAsync));
            return result.MakeMultiSigResponse.Result;
        }

        /// <summary>
        /// Export multisig info for other participants.
        /// </summary>
        /// <returns>A string representing multisig information.</returns>
        public async Task<string> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ExportMultiSigInfoAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportMultiSigInfoResponse, nameof(this.ExportMultiSigInfoAsync));
            return result.ExportMultiSigInfoResponse.Result.Information;
        }

        /// <summary>
        /// Import multisig info from other participants.
        /// </summary>
        /// <param name="info"> List of multisig info in hex format from other participants.</param>
        public async Task<ImportMultiSigInformation> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ImportMultiSigInfoAsync(info, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportMultiSigInfoResponse, nameof(this.ImportMultiSigInfoAsync));
            return result.ImportMultiSigInfoResponse.Result;
        }

        /// <summary>
        /// Turn this wallet into a multisig wallet, extra step for N-1/N wallets.
        /// </summary>
        /// <param name="multiSigInfo">List of multisig string from peers.</param>
        /// <param name="password">Wallet password.</param>
        /// <returns>The multisig wallet address.</returns>
        public async Task<string> FinalizeMultiSigAsync(IEnumerable<string> multiSigInfo, string password, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.FinalizeMultiSigAsync(multiSigInfo, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FinalizeMultiSigResponse, nameof(this.FinalizeMultiSigAsync));
            return result.FinalizeMultiSigResponse.Result.Address;
        }

        /// <summary>
        /// Sign a transaction in multisig.
        /// </summary>
        /// <param name="txDataHex">Multisig transaction in hex format, as returned by transfer under multisig_txset.</param>
        public async Task<SignMultiSigTransaction> SignMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SignMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignMultiSigTransactionResponse, nameof(this.SignMultiSigAsync));
            return result.SignMultiSigTransactionResponse.Result;
        }

        /// <summary>
        /// Submit a signed multisig transaction.
        /// </summary>
        /// <param name="txDataHex">Multisig transaction in hex format, as returned by sign_multisig under tx_data_hex.</param>
        /// <returns>List of transaction hashes.</returns>
        public async Task<List<string>> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SubmitMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitMultiSigTransactionResponse, nameof(this.SubmitMultiSigAsync));
            return result.SubmitMultiSigTransactionResponse.Result.TransactionHashes;
        }

        /// <summary>
        /// Parse a transfer that is either multi-signature, or unsigned from a cold-wallet.
        /// </summary>
        public async Task<List<TransferDescription>> DescribeUnsignedTransferAsync(string unsignedTxSet, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.DescribeTransferAsync(unsignedTxSet, false, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DescribeTransferResponse, nameof(this.DescribeUnsignedTransferAsync));
            return result.DescribeTransferResponse.Result.TransferDescriptions;
        }

        /// <summary>
        /// Parse a transfer that is either multi-signature, or unsigned from a cold-wallet.
        /// </summary>
        public async Task<List<TransferDescription>> DescribeMultiSigTransferAsync(string multiSigTxSet, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.DescribeTransferAsync(multiSigTxSet, true, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DescribeTransferResponse, nameof(this.DescribeMultiSigTransferAsync));
            return result.DescribeTransferResponse.Result.TransferDescriptions;
        }

        /// <summary>
        /// Send all dust outputs back to the wallet's, to make them easier to spend (and mix).
        /// </summary>
        public async Task<SweepSingle> SweepSingleAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, bool get_tx_metadata = true, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SweepSingleAsync(address, account_index, transaction_priority, ring_size, unlock_time, get_tx_key, get_tx_hex, get_tx_metadata, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepSingleResponse, nameof(this.SweepSingleAsync));
            return result.SweepSingleResponse.Result;
        }

        /// <summary>
        /// Retrieve the payment details associated with a given payment id.
        /// </summary>
        public async Task<List<PaymentDetail>> GetPaymentDetailAsync(string payment_id, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetPaymentDetailAsync(payment_id, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.PaymentDetailResponse, nameof(this.GetPaymentDetailAsync));
            return result.PaymentDetailResponse.Result.Payments;
        }

        /// <summary>
        /// Set an attribute (key/value pair) to store any additional info in the wallet.
        /// </summary>
        public async Task SetAttributeAsync(string key, string value, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.SetAttributeAsync(key, value, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetAttributeResponse, nameof(this.SetAttributeAsync));
        }

        /// <summary>
        /// Get an attribute value from the wallet, given its key; throws an error if not present.
        /// </summary>
        public async Task<string> GetAttributeAsync(string key, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.GetAttributeAsync(key, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetAttributeResponse, nameof(this.GetAttributeAsync));
            return result.GetAttributeResponse.Result.Value;
        }

        /// <summary>
        /// Analyzes a string to determine whether it is a valid monero wallet address and returns the result and the address specifications.
        /// </summary>
        /// <param name="address">The address to validate.</param>
        /// <param name="any_net_type">If true, consider addresses belonging to any of the three Monero networks (mainnet, stagenet, and testnet) valid. Otherwise, only consider an address valid if it belongs to the network on which the rpc-wallet's current daemon is running (Defaults to false).</param>
        /// <param name="allow_openalias">If true, consider OpenAlias-formatted addresses valid (Defaults to false).</param>
        /// <param name="token">A CancellationToken.</param>
        public async Task<ValidateAddress> ValidateAddressAsync(string address, bool any_net_type = false, bool allow_openalias = false, CancellationToken token = default)
        {
            var result = await this.moneroRpcCommunicator.ValidateAddressAsync(address, any_net_type, allow_openalias, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ValidateAddressResponse, nameof(this.ValidateAddressAsync));
            return result.ValidateAddressResponse.Result;
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (this.disposingLock)
            {
                if (this.disposed)
                {
                    return;
                }
                else
                {
                    this.disposed = true;
                }
            }

            if (disposing)
            {
                // Free managed objects.
                this.SaveWalletAsync().GetAwaiter().GetResult();
                this.CloseWalletAsync().GetAwaiter().GetResult();
                this.moneroRpcCommunicator.Dispose();
            }

            // Free unmanaged objects.
        }

        private async Task<MoneroWalletClient> InitializeAsync(string filename, string password, CancellationToken cancellationToken)
        {
            await this.OpenWalletAsync(filename, password, cancellationToken).ConfigureAwait(false);
            return this;
        }
    }
}