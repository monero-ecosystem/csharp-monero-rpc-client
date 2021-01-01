using Monero.Client.Network;
using Monero.Client.Utilities;
using Monero.Client.Wallet.POD;
using Monero.Client.Wallet.POD.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Monero.Client.Wallet
{
    public class MoneroWalletClient : IMoneroWalletClient
    {
        private readonly RpcCommunicator _moneroRpcCommunicator;

        public MoneroWalletClient(Uri uri)
        {
            _moneroRpcCommunicator = new RpcCommunicator(uri);
        }

        public MoneroWalletClient(Uri uri, HttpMessageHandler httpMessageHandler)
        {
            _moneroRpcCommunicator = new RpcCommunicator(uri, httpMessageHandler);
        }

        public MoneroWalletClient(Uri uri, HttpMessageHandler httpMessageHandler, bool disposeHandler)
        {
            _moneroRpcCommunicator = new RpcCommunicator(uri, httpMessageHandler, disposeHandler);
        }

        /// <summary>
        /// Initialize a Monero Wallet Client using default network settings (<localhost>:<defaultport>)
        /// </summary>
        public MoneroWalletClient(MoneroNetwork networkType)
        {
            _moneroRpcCommunicator = new RpcCommunicator(networkType, ConnectionType.Wallet);
        }

        /// <summary>
        /// Initialize a Monero Wallet Client using default network settings (<localhost>:<defaultport>), opening the wallet while doing so.
        /// </summary>
        public static Task<MoneroWalletClient> CreateAsync(Uri uri, string filename, string password, CancellationToken cancellationToken = default)
        {
            var moneroWalletClient = new MoneroWalletClient(uri);
            return moneroWalletClient.InitializeAsync(filename, password, cancellationToken);
        }

        public static Task<MoneroWalletClient> CreateAsync(MoneroNetwork networkType, string filename, string password, CancellationToken cancellationToken = default)
        {
            var moneroWalletClient = new MoneroWalletClient(networkType);
            return moneroWalletClient.InitializeAsync(filename, password, cancellationToken);
        }

        private async Task<MoneroWalletClient> InitializeAsync(string filename, string password, CancellationToken cancellationToken)
        {
            await OpenWalletAsync(filename, password, cancellationToken).ConfigureAwait(false);
            return this;
        }

        /// <summary>
        /// Disposes the object (also calls <see cref="CloseWalletAsync(CancellationToken)"/>)
        /// </summary>
        public void Dispose()
        {
            this.CloseWalletAsync().GetAwaiter().GetResult();
            _moneroRpcCommunicator.Dispose();
        }

        /// <summary>
        /// Returns wallet's balance.
        /// </summary>
        public async Task<Balance> GetBalanceAsync(uint accountIndex, IEnumerable<uint> addressIndices, bool allAccounts = false, bool strict = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBalanceAsync(accountIndex, addressIndices, allAccounts, strict, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BalanceResponse, nameof(GetBalanceAsync));
            return result.BalanceResponse.Result;
        }

        /// <summary>
        /// Returns wallet's balance for a given account index.
        /// </summary>
        public async Task<Balance> GetBalanceAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBalanceAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BalanceResponse, nameof(GetBalanceAsync));
            return result.BalanceResponse.Result;
        }

        /// <summary>
        /// Return the wallet's addresses for an account.
        /// </summary>
        public async Task<Addresses> GetAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAddressAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressResponse, nameof(GetAddressAsync));
            return result.AddressResponse.Result;
        }

        /// <summary>
        /// <seealso cref="GetAddressAsync(uint, CancellationToken)"/> Also filter for specific set of subaddresses.
        /// </summary>
        public async Task<Addresses> GetAddressAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAddressAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressResponse, nameof(GetAddressAsync));
            return result.AddressResponse.Result;
        }

        /// <summary>
        /// Get account and address indexes from a specific (sub)address
        /// </summary>
        public async Task<AddressIndex> GetAddressIndexAsync(string address, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAddressIndexAsync(address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressIndexResponse, nameof(GetAddressIndexAsync));
            return result.AddressIndexResponse.Result;
        }

        /// <summary>
        /// Create a new address for an account.
        /// </summary>
        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateAddressAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressCreationResponse, nameof(CreateAddressAsync));
            return result.AddressCreationResponse.Result;
        }

        /// <summary>
        ///  <seealso cref="CreateAccountAsync(CancellationToken)"/> Also label the new address.
        /// </summary>
        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateAddressAsync(accountIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressCreationResponse, nameof(CreateAddressAsync));
            return result.AddressCreationResponse.Result;
        }

        /// <summary>
        /// Label an address.
        /// </summary>
        public async Task<AddressLabel> LabelAddressAsync(uint majorIndex, uint minorIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.LabelAddressAsync(majorIndex, minorIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressLabelResponse, nameof(LabelAddressAsync));
            return result.AddressLabelResponse.Result;
        }

        /// <summary>
        /// Get all accounts for a wallet.
        /// </summary>
        public async Task<Account> GetAccountsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAccountsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountResponse, nameof(GetAccountsAsync));
            return result.AccountResponse.Result;
        }

        /// <summary>
        /// <seealso cref="GetAccountsAsync(CancellationToken)"/> Also filter accounts by tag.
        /// </summary>
        public async Task<Account> GetAccountsAsync(string tag, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAccountsAsync(tag, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountResponse, nameof(GetAccountsAsync));
            return result.AccountResponse.Result;
        }

        /// <summary>
        /// Create a new account.
        /// </summary>
        public async Task<CreateAccount> CreateAccountAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateAccountAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateAccountResponse, nameof(CreateAccountAsync));
            return result.CreateAccountResponse.Result;
        }

        /// <summary>
        /// <seealso cref="CreateAccountAsync(CancellationToken)"/> Doing so with an optional label.
        /// </summary>
        public async Task<CreateAccount> CreateAccountAsync(string label, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateAccountAsync(label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateAccountResponse, nameof(CreateAccountAsync));
            return result.CreateAccountResponse.Result;
        }

        /// <summary>
        /// Label an account.
        /// </summary>
        public async Task<AccountLabel> LabelAccountAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.LabelAccountAsync(accountIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountLabelResponse, nameof(LabelAccountAsync));
            return result.AccountLabelResponse.Result;
        }

        /// <summary>
        /// Get a list of user-defined account tags.
        /// </summary>
        public async Task<AccountTags> GetAccountTagsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAccountTagsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountTagsResponse, nameof(GetAccountTagsAsync));
            return result.AccountTagsResponse.Result;
        }

        /// <summary>
        /// Apply a filtering tag to a list of accounts.
        /// </summary>
        public async Task<TagAccounts> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TagAccountsAsync(tag, accounts, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TagAccountsResponse, nameof(TagAccountsAsync));
            return result.TagAccountsResponse.Result;
        }

        /// <summary>
        /// Remove filtering tag from a list of accounts.
        /// </summary>
        public async Task<UntagAccounts> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.UntagAccountsAsync(accounts, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.UntagAccountsResponse, nameof(UntagAccountsAsync));
            return result.UntagAccountsResponse.Result;
        }

        /// <summary>
        /// Set description for an account tag.
        /// </summary>
        public async Task<AccountTagAndDescription> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SetAccountTagDescriptionAsync(tag, description, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetAccountTagAndDescriptionResponse, nameof(SetAccountTagDescriptionAsync));
            return result.SetAccountTagAndDescriptionResponse.Result;
        }

        /// <summary>
        /// Returns the wallet's current block height.
        /// </summary>
        public async Task<ulong> GetHeightAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetHeightAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockchainHeightResponse, nameof(GetHeightAsync));
            return result.BlockchainHeightResponse.Result.Height;
        }

        /// <summary>
        /// Send monero to a number of recipients.
        /// </summary>
        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferAsync(transactions, transferPriority, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        /// <summary>
        /// <seealso cref="IMoneroWalletClient.TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, uint unlockTime = 0, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferAsync(transactions, transferPriority, getTxKey, getTxHex, unlockTime, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        /// <summary>
        /// <seealso cref="IMoneroWalletClient.TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferAsync(transactions, transferPriority, ringSize, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        /// <summary>
        /// <seealso cref="IMoneroWalletClient.TransferAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, CancellationToken)"/>
        /// </summary>
        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferAsync(transactions, transferPriority, ringSize, accountIndex, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        /// <summary>
        /// Same as transfer, but can split into more than one tx if necessary.
        /// </summary>
        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool newAlgorithm = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, newAlgorithm, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        /// <summary>
        /// <see cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>
        /// </summary>
        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, bool newAlgorithm = true, uint unlockTime = 0, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, getTxKey, getTxHex, newAlgorithm, unlockTime, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        /// <summary>
        /// <see cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>
        /// </summary>
        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, ringSize, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        /// <summary>
        /// <see cref="TransferSplitAsync(IEnumerable{(string address, ulong amount)}, TransferPriority, bool, CancellationToken)"/>
        /// </summary>
        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, ringSize, accountIndex, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        /// <summary>
        /// Sign a transaction created on a read-only wallet (in cold-signing process).
        /// </summary>
        public async Task<SignTransfer> SignTransferAsync(string unsignedTxSet, bool exportRaw = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SignTransferAsync(unsignedTxSet, exportRaw, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignTransferResponse, nameof(SignTransferAsync));
            return result.SignTransferResponse.Result;
        }

        /// <summary>
        /// Submit a previously signed transaction on a read-only wallet (in cold-signing process).
        /// </summary>
        /// <returns>A list of transaction hashes.</returns>
        public async Task<List<string>> SubmitTransferAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SubmitTransferAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitTransferResponse, nameof(SubmitTransferAsync));
            return result.SubmitTransferResponse.Result.TransactionHashes;
        }

        /// <summary>
        /// Send all dust outputs back to the wallet's, to make them easier to spend (and mix).
        /// </summary>
        public async Task<SweepDust> SweepDustAsync(bool getTxKey, bool getTxHex, bool getTxMetadata, bool doNotRelay = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SweepDustAsync(getTxKey, getTxHex, getTxMetadata, doNotRelay, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepDustResponse, nameof(SweepDustAsync));
            return result.SweepDustResponse.Result;
        }

        /// <summary>
        /// Send all unlocked balance to an address. Be careful...
        /// </summary>
        public async Task<SplitFundTransfer> SweepAllAsync(string address, uint accountIndex, TransferPriority transactionPriority, uint ringSize, ulong unlockTime = 0, ulong belowAmount = ulong.MaxValue, bool getTxKeys = true, bool getTxHex = true, bool getTxMetadata = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SweepAllAsync(address, accountIndex, transactionPriority, ringSize, unlockTime, belowAmount, getTxKeys, getTxHex, getTxMetadata, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepAllResponse, nameof(SweepAllAsync));
            return result.SweepAllResponse.Result;
        }

        /// <summary>
        /// Save the wallet.
        /// </summary>
        public async Task<SaveWallet> SaveWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SaveWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SaveWalletResponse, nameof(SaveWalletAsync));
            return result.SaveWalletResponse.Result;
        }

        /// <summary>
        /// Return a list of incoming transfers to the wallet.
        /// </summary>
        public async Task<List<IncomingTransfer>> GetIncomingTransfersAsync(TransferType transferType, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result.Transfers;
        }

        /// <summary>
        /// <seealso cref="GetIncomingTransfersAsync(TransferType, bool, CancellationToken)"/>
        /// </summary>
        public async Task<List<IncomingTransfer>> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, accountIndex, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result.Transfers;
        }

        /// <summary>
        /// <seealso cref="GetIncomingTransfersAsync(TransferType, bool, CancellationToken)"/>
        /// </summary>
        public async Task<List<IncomingTransfer>> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, IEnumerable<uint> subaddrIndices, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, accountIndex, subaddrIndices, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result.Transfers;
        }

        /// <summary>
        /// Return the spend or view private key.
        /// </summary>
        public async Task<string> GetPrivateKey(KeyType keyType, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetPrivateKey(keyType, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.QueryKeyResponse, nameof(GetPrivateKey));
            return result.QueryKeyResponse.Result.Key;
        }

        /// <summary>
        /// Stops the wallet, storing the current state.
        /// </summary>
        public async Task<StopWalletResult> StopWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.StopWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.StopWalletResponse, nameof(StopWalletAsync));
            return result.StopWalletResponse.Result;
        }

        /// <summary>
        /// Set arbitrary string notes for transaction.
        /// </summary>
        public async Task<SetTransactionNotes> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SetTransactionNotesAsync(txids, notes, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetTransactionNotesResponse, nameof(SetTransactionNotesAsync));
            return result.SetTransactionNotesResponse.Result;
        }

        /// <summary>
        /// Get string notes for transactions of interest.
        /// </summary>
        public async Task<List<string>> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransactionNotesAsync(txids, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetTransactionNotesResponse, nameof(GetTransactionNotesAsync));
            return result.GetTransactionNotesResponse.Result.Notes;
        }

        /// <summary>
        /// Get transaction secret key from transaction id.
        /// </summary>
        public async Task<string> GetTransactionKeyAsync(string txid, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransactionKeyAsync(txid, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetTransactionKeyResponse, nameof(GetTransactionKeyAsync));
            return result.GetTransactionKeyResponse.Result.TransactionKey;
        }

        /// <summary>
        /// Check a transaction in the blockchain with its secret key.
        /// </summary>
        public async Task<CheckTransactionKey> CheckTransactionKeyAsync(string txid, string txKey, string address, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CheckTransactionKeyAsync(txid, txKey, address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CheckTransactionKeyResponse, nameof(CheckTransactionKeyAsync));
            return result.CheckTransactionKeyResponse.Result;
        }

        /// <summary>
        /// Returns a list of transfers from one or more of the desired categories.
        /// </summary>
        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransfersAsync(@in, @out, pending, failed, pool, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ShowTransfersResponse, nameof(GetTransfersAsync));
            return result.ShowTransfersResponse.Result;
        }

        /// <summary>
        /// <seealso cref="GetTransfersAsync(bool, bool, bool, bool, bool, CancellationToken)"/>
        /// </summary>
        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint minHeight, uint maxHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransfersAsync(@in, @out, pending, failed, pool, minHeight, maxHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ShowTransfersResponse, nameof(GetTransfersAsync));
            return result.ShowTransfersResponse.Result;
        }

        /// <summary>
        /// <seealso cref="GetTransferByTxidAsync(string, uint, CancellationToken)"/>
        /// </summary>
        public async Task<Transfer> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransferByTxidAsync(txid, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransferByTxidResponse, nameof(GetTransferByTxidAsync));
            return result.TransferByTxidResponse.Result.Transfer;
        }

        /// <summary>
        /// Show information about a transfer to/from this address.
        /// </summary>
        public async Task<Transfer> GetTransferByTxidAsync(string txid, uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransferByTxidAsync(txid, accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransferByTxidResponse, nameof(GetTransferByTxidAsync));
            return result.TransferByTxidResponse.Result.Transfer;
        }

        /// <summary>
        /// Sign a string.
        /// </summary>
        public async Task<string> SignAsync(string data, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SignAsync(data, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignResponse, nameof(SignAsync));
            return result.SignResponse.Result.Sig;
        }

        ///// <summary>
        ///// Verify a signature on a string.
        ///// </summary>
        ///// <returns>Whether the signature is valid.</returns>
        //public async Task<bool> VerifyAsync(string data, string address, string signature, CancellationToken token = default)
        //{
        //    var result = await _moneroRpcCommunicator.VerifyAsync(data, address, signature, token).ConfigureAwait(false);
        //    ErrorGuard.ThrowIfResultIsNull(result?.VerifyResponse, nameof(VerifyAsync));
        //    return result.VerifyResponse.Result.IsGood;
        //}

        /// <summary>
        /// Export outputs in hex format.
        /// </summary>
        public async Task<string> ExportOutputsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ExportOutputsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportOutputsResponse, nameof(ExportOutputsAsync));
            return result.ExportOutputsResponse.Result.OutputsDataHex;
        }

        /// <summary>
        /// Import outputs in hex format.
        /// </summary>
        /// <returns>The number of outputs imported.</returns>
        public async Task<ulong> ImportOutputsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ImportOutputsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportOutputsResponse, nameof(ImportOutputsAsync));
            return result.ImportOutputsResponse.Result.NumImported;
        }

        /// <summary>
        /// Export a signed set of key images.
        /// </summary>
        public async Task<List<SignedKeyImage>> ExportKeyImagesAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ExportKeyImagesAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportKeyImagesResponse, nameof(ExportKeyImagesAsync));
            return result.ExportKeyImagesResponse.Result.SignedKeyImages;
        }

        /// <summary>
        /// Import signed key images list and verify their spent status.
        /// </summary>
        public async Task<ImportKeyImages> ImportKeyImagesAsync(IEnumerable<(string keyImage, string signature)> signedKeyImages, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ImportKeyImagesAsync(signedKeyImages, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportKeyImagesResponse, nameof(ImportKeyImagesAsync));
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
        public async Task<string> MakeUriAsync(string address, ulong amount, string recipientName, string txDescription = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.MakeUriAsync(address, amount, recipientName, txDescription, paymentId, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MakeUriResponse, nameof(MakeUriAsync));
            return result.MakeUriResponse.Result.Uri;
        }

        /// <summary>
        /// Parse a payment URI to get payment information.
        /// </summary>
        public async Task<MoneroUri> ParseUriAsync(string uri, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ParseUriAsync(uri, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ParseUriResponse, nameof(ParseUriAsync));
            return result.ParseUriResponse.Result.Uri;
        }

        /// <summary>
        /// Retrieves entries from the address book.
        /// </summary>
        /// <param name="entires">Indices of the requested address book entries.</param>
        public async Task<List<AddressBookEntry>> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAddressBookAsync(entries, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetAddressBookResponse, nameof(GetAddressBookAsync));
            return result.GetAddressBookResponse.Result.Entries;
        }

        /// <summary>
        /// Add an entry to the address book.
        /// </summary>
        public async Task<AddAddressBook> AddAddressBookAsync(string address, string description = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.AddAddressBookAsync(address, description, paymentId, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddAddressBookResponse, nameof(AddAddressBookAsync));
            return result.AddAddressBookResponse.Result;
        }

        /// <summary>
        /// Delete an entry from ther address book.
        /// </summary>
        public async Task<DeleteAddressBook> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.DeleteAddressBookAsync(index, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DeleteAddressBookResponse, nameof(DeleteAddressBookAsync));
            return result.DeleteAddressBookResponse.Result;
        }

        /// <summary>
        /// Refresh a wallet after opening.
        /// </summary>
        public async Task<RefreshWallet> RefreshWalletAsync(uint startHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.RefreshWalletAsync(startHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RefreshWalletResponse, nameof(RefreshWalletAsync));
            return result.RefreshWalletResponse.Result;
        }

        /// <summary>
        /// Rescan the blockchain for spent outputs.
        /// </summary>
        public async Task<RescanSpent> RescanSpentAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.RescanSpentAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RescanSpentResponse, nameof(RescanSpentAsync));
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
            var result = await _moneroRpcCommunicator.CreateWalletAsync(filename, language, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateWalletResponse, nameof(CreateWalletAsync));
            return result.CreateWalletResponse.Result;
        }

        /// <summary>
        /// Open a wallet. You need to have set the argument "–wallet-dir" when launching monero-wallet-rpc to make this work.
        /// </summary>
        public async Task<OpenWallet> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.OpenWalletAsync(filename, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.OpenWalletResponse, nameof(OpenWalletAsync));
            return result.OpenWalletResponse.Result;
        }

        /// <summary>
        /// Close the currently opened wallet, after trying to save it.
        /// Note: After you close a wallet, every subsequent query will fail.
        /// </summary>
        public async Task<CloseWallet> CloseWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CloseWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CloseWalletResponse, nameof(CloseWalletAsync));
            return result.CloseWalletResponse.Result;
        }

        /// <summary>
        /// Change a wallet password.
        /// </summary>
        public async Task<ChangeWalletPassword> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ChangeWalletPasswordAsync(oldPassword, newPassword, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ChangeWalletPasswordResponse, nameof(ChangeWalletPasswordAsync));
            return result.ChangeWalletPasswordResponse.Result;
        }

        /// <summary>
        /// Get RPC version Major & Minor integer-format, where Major is the first 16 bits and Minor the last 16 bits.
        /// </summary>
        public async Task<uint> GetVersionAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetRpcVersionAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetRpcVersionResponse, nameof(GetVersionAsync));
            return result.GetRpcVersionResponse.Result.Version;
        }

        /// <summary>
        /// Check if a wallet is a multi-signature (multisig) one.
        /// </summary>
        public async Task<IsMultiSigInformation> IsMultiSigAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.IsMultiSigAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IsMultiSigInformationResponse, nameof(IsMultiSigAsync));
            return result.IsMultiSigInformationResponse.Result;
        }

        /// <summary>
        /// Prepare a wallet for multisig by generating a multisig string to share with peers.
        /// </summary>
        /// <returns>A string representing multisig information.</returns>
        public async Task<string> PrepareMultiSigAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.PrepareMultiSigAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.PrepareMultiSigResponse, nameof(PrepareMultiSigAsync));
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
            var result = await _moneroRpcCommunicator.MakeMultiSigAsync(multiSigInfo, threshold, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MakeMultiSigResponse, nameof(MakeMultiSigAsync));
            return result.MakeMultiSigResponse.Result;
        }

        /// <summary>
        /// Export multisig info for other participants.
        /// </summary>
        public async Task<string> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ExportMultiSigInfoAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportMultiSigInfoResponse, nameof(ExportMultiSigInfoAsync));
            return result.ExportMultiSigInfoResponse.Result.Information;
        }

        /// <summary>
        /// Import multisig info from other participants.
        /// </summary>
        /// <param name="info"> List of multisig info in hex format from other participants.</param>
        public async Task<ImportMultiSigInformation> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ImportMultiSigInfoAsync(info, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportMultiSigInfoResponse, nameof(ImportMultiSigInfoAsync));
            return result.ImportMultiSigInfoResponse.Result;
        }

        /// <summary>
        /// Turn this wallet into a multisig wallet, extra step for N-1/N wallets.
        /// </summary>
        /// <param name="multisigInfo">List of multisig string from peers.</param>
        /// <param name="password">Wallet password.</param>
        public async Task<string> FinalizeMultiSigAsync(IEnumerable<string> multiSigInfo, string password, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.FinalizeMultiSigAsync(multiSigInfo, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FinalizeMultiSigResponse, nameof(FinalizeMultiSigAsync));
            return result.FinalizeMultiSigResponse.Result.Address;
        }

        /// <summary>
        /// Sign a transaction in multisig.
        /// </summary>
        /// <param name="txDataHex">Multisig transaction in hex format, as returned by transfer under multisig_txset.</param>
        public async Task<SignMultiSigTransaction> SignMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SignMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignMultiSigTransactionResponse, nameof(SignMultiSigAsync));
            return result.SignMultiSigTransactionResponse.Result;
        }

        /// <summary>
        /// Submit a signed multisig transaction.
        /// </summary>
        /// <param name="txDataHex">Multisig transaction in hex format, as returned by sign_multisig under tx_data_hex.</param>
        /// <returns>List of transaction hashes.</returns>
        public async Task<List<string>> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SubmitMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitMultiSigTransactionResponse, nameof(SubmitMultiSigAsync));
            return result.SubmitMultiSigTransactionResponse.Result.TransactionHashes;
        }

        /// <summary>
        /// Parse a transfer that is either multi-signature, or unsigned from a cold-wallet.
        /// </summary>
        public async Task<List<TransferDescription>> DescribeUnsignedTransferAsync(string unsignedTxSet, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.DescribeTransferAsync(unsignedTxSet, false, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DescribeTransferResponse, nameof(DescribeUnsignedTransferAsync));
            return result.DescribeTransferResponse.Result.TransferDescriptions;
        }

        /// <summary>
        /// Parse a transfer that is either multi-signature, or unsigned from a cold-wallet.
        /// </summary>
        public async Task<List<TransferDescription>> DescribeMultiSigTransferAsync(string multiSigTxSet, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.DescribeTransferAsync(multiSigTxSet, true, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DescribeTransferResponse, nameof(DescribeMultiSigTransferAsync));
            return result.DescribeTransferResponse.Result.TransferDescriptions;
        }

        /// <summary>
        /// Send all dust outputs back to the wallet's, to make them easier to spend (and mix).
        /// </summary>
        public async Task<SweepSingle> SweepSingleAsync(string address, uint account_index, TransferPriority transaction_priority, uint ring_size, ulong unlock_time = 0, bool get_tx_key = true, bool get_tx_hex = true, bool get_tx_metadata = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SweepSingleAsync(address, account_index, transaction_priority, ring_size, unlock_time, get_tx_key, get_tx_hex, get_tx_metadata, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepSingleResponse, nameof(SweepSingleAsync));
            return result.SweepSingleResponse.Result;
        }

        /// <summary>
        /// Retrieve the payment details associated with a given payment id.
        /// </summary>
        public async Task<List<PaymentDetail>> GetPaymentDetailAsync(string payment_id, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetPaymentDetailAsync(payment_id, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.PaymentDetailResponse, nameof(GetPaymentDetailAsync));
            return result.PaymentDetailResponse.Result.Payments;
        }
    }
}