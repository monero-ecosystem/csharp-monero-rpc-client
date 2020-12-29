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

        public async Task<Balance> GetBalanceAsync(uint accountIndex, IEnumerable<uint> addressIndices, bool allAccounts = false, bool strict = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBalanceAsync(accountIndex, addressIndices, allAccounts, strict, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BalanceResponse, nameof(GetBalanceAsync));
            return result.BalanceResponse.Result;
        }

        public async Task<Balance> GetBalanceAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetBalanceAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BalanceResponse, nameof(GetBalanceAsync));
            return result.BalanceResponse.Result;
        }

        public async Task<AddressResult> GetAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAddressAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressResponse, nameof(GetAddressAsync));
            return result.AddressResponse.Result;
        }

        public async Task<AddressResult> GetAddressAsync(uint accountIndex, IEnumerable<uint> addressIndices, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAddressAsync(accountIndex, addressIndices, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressResponse, nameof(GetAddressAsync));
            return result.AddressResponse.Result;
        }

        public async Task<AddressIndex> GetAddressIndexAsync(string address, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAddressIndexAsync(address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressIndexResponse, nameof(GetAddressIndexAsync));
            return result.AddressIndexResponse.Result;
        }

        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateAddressAsync(accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressCreationResponse, nameof(CreateAddressAsync));
            return result.AddressCreationResponse.Result;
        }

        public async Task<AddressCreation> CreateAddressAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateAddressAsync(accountIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressCreationResponse, nameof(CreateAddressAsync));
            return result.AddressCreationResponse.Result;
        }

        public async Task<AddressLabel> LabelAddressAsync(uint majorIndex, uint minorIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.LabelAddressAsync(majorIndex, minorIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddressLabelResponse, nameof(LabelAddressAsync));
            return result.AddressLabelResponse.Result;
        }

        public async Task<AccountResult> GetAccountsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAccountsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountResponse, nameof(GetAccountsAsync));
            return result.AccountResponse.Result;
        }

        public async Task<AccountResult> GetAccountsAsync(string tag, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAccountsAsync(tag, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountResponse, nameof(GetAccountsAsync));
            return result.AccountResponse.Result;
        }

        public async Task<CreateAccount> CreateAccountAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateAccountAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateAccountResponse, nameof(CreateAccountAsync));
            return result.CreateAccountResponse.Result;
        }

        public async Task<CreateAccount> CreateAccountAsync(string label, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateAccountAsync(label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateAccountResponse, nameof(CreateAccountAsync));
            return result.CreateAccountResponse.Result;
        }

        public async Task<AccountLabel> LabelAccountAsync(uint accountIndex, string label, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.LabelAccountAsync(accountIndex, label, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountLabelResponse, nameof(LabelAccountAsync));
            return result.AccountLabelResponse.Result;
        }

        public async Task<AccountTags> GetAccountTagsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAccountTagsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AccountTagsResponse, nameof(GetAccountTagsAsync));
            return result.AccountTagsResponse.Result;
        }

        public async Task<TagAccounts> TagAccountsAsync(string tag, IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TagAccountsAsync(tag, accounts, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TagAccountsResponse, nameof(TagAccountsAsync));
            return result.TagAccountsResponse.Result;
        }

        public async Task<UntagAccounts> UntagAccountsAsync(IEnumerable<uint> accounts, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.UntagAccountsAsync(accounts, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.UntagAccountsResponse, nameof(UntagAccountsAsync));
            return result.UntagAccountsResponse.Result;
        }

        public async Task<AccountTagAndDescription> SetAccountTagDescriptionAsync(string tag, string description, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SetAccountTagDescriptionAsync(tag, description, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetAccountTagAndDescriptionResponse, nameof(SetAccountTagDescriptionAsync));
            return result.SetAccountTagAndDescriptionResponse.Result;
        }

        public async Task<BlockchainHeight> GetHeightAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetHeightAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.BlockchainHeightResponse, nameof(GetHeightAsync));
            return result.BlockchainHeightResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferAsync(transactions, transferPriority, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, uint unlockTime = 0, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferAsync(transactions, transferPriority, getTxKey, getTxHex, unlockTime, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferAsync(transactions, transferPriority, ringSize, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        public async Task<FundTransfer> TransferAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {;
            var result = await _moneroRpcCommunicator.TransferAsync(transactions, transferPriority, ringSize, accountIndex, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferResponse, nameof(TransferAsync));
            return result.FundTransferResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool newAlgorithm = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, newAlgorithm, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, bool getTxKey, bool getTxHex, bool newAlgorithm = true, uint unlockTime = 0, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, getTxKey, getTxHex, newAlgorithm, unlockTime, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, ringSize, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SplitFundTransfer> TransferSplitAsync(IEnumerable<(string address, ulong amount)> transactions, TransferPriority transferPriority, uint ringSize, uint accountIndex, bool newAlgorithm = true, uint unlockTime = 0, bool getTxKey = true, bool getTxHex = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.TransferSplitAsync(transactions, transferPriority, ringSize, accountIndex, newAlgorithm, unlockTime, getTxKey, getTxHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FundTransferSplitResponse, nameof(TransferSplitAsync));
            return result.FundTransferSplitResponse.Result;
        }

        public async Task<SignTransfer> SignTransferAsync(string unsignedTxSet, bool exportRaw = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SignTransferAsync(unsignedTxSet, exportRaw, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignTransferResponse, nameof(SignTransferAsync));
            return result.SignTransferResponse.Result;
        }

        public async Task<SubmitTransfer> SubmitTransferAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SubmitTransferAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitTransferResponse, nameof(SubmitTransferAsync));
            return result.SubmitTransferResponse.Result;
        }

        public async Task<SweepDust> SweepDustAsync(bool getTxKey, bool getTxHex, bool getTxMetadata, bool doNotRelay = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SweepDustAsync(getTxKey, getTxHex, getTxMetadata, doNotRelay, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepDustResponse, nameof(SweepDustAsync));
            return result.SweepDustResponse.Result;
        }

        public async Task<SplitFundTransfer> SweepAllAsync(string address, uint accountIndex, TransferPriority transactionPriority, uint ringSize, ulong unlockTime = 0, ulong belowAmount = ulong.MaxValue, bool getTxKeys = true, bool getTxHex = true, bool getTxMetadata = true, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SweepAllAsync(address, accountIndex, transactionPriority, ringSize, unlockTime, belowAmount, getTxKeys, getTxHex, getTxMetadata, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SweepAllResponse, nameof(SweepAllAsync));
            return result.SweepAllResponse.Result;
        }

        public async Task<SaveWallet> SaveWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SaveWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SaveWalletResponse, nameof(SaveWalletAsync));
            return result.SaveWalletResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, accountIndex, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<IncomingTransfers> GetIncomingTransfersAsync(TransferType transferType, uint accountIndex, IEnumerable<uint> subaddrIndices, bool returnKeyImage = false, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetIncomingTransfersAsync(transferType, accountIndex, subaddrIndices, returnKeyImage, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IncomingTransfersResponse, nameof(GetIncomingTransfersAsync));
            return result.IncomingTransfersResponse.Result;
        }

        public async Task<QueryKey> GetPrivateKey(KeyType keyType, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetPrivateKey(keyType, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.QueryKeyResponse, nameof(GetPrivateKey));
            return result.QueryKeyResponse.Result;
        }

        public async Task<StopWalletResult> StopWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.StopWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.StopWalletResponse, nameof(StopWalletAsync));
            return result.StopWalletResponse.Result;
        }

        public async Task<SetTransactionNotes> SetTransactionNotesAsync(IEnumerable<string> txids, IEnumerable<string> notes, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SetTransactionNotesAsync(txids, notes, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SetTransactionNotesResponse, nameof(SetTransactionNotesAsync));
            return result.SetTransactionNotesResponse.Result;
        }

        public async Task<GetTransactionNotes> GetTransactionNotesAsync(IEnumerable<string> txids, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransactionNotesAsync(txids, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetTransactionNotesResponse, nameof(GetTransactionNotesAsync));
            return result.GetTransactionNotesResponse.Result;
        }

        public async Task<GetTransactionKey> GetTransactionKeyAsync(string txid, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransactionKeyAsync(txid, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetTransactionKeyResponse, nameof(GetTransactionKeyAsync));
            return result.GetTransactionKeyResponse.Result;
        }

        public async Task<CheckTransactionKey> CheckTransactionKeyAsync(string txid, string txKey, string address, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CheckTransactionKeyAsync(txid, txKey, address, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CheckTransactionKeyResponse, nameof(CheckTransactionKeyAsync));
            return result.CheckTransactionKeyResponse.Result;
        }

        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransfersAsync(@in, @out, pending, failed, pool, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ShowTransfersResponse, nameof(GetTransfersAsync));
            return result.ShowTransfersResponse.Result;
        }

        public async Task<ShowTransfers> GetTransfersAsync(bool @in, bool @out, bool pending, bool failed, bool pool, uint minHeight, uint maxHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransfersAsync(@in, @out, pending, failed, pool, minHeight, maxHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ShowTransfersResponse, nameof(GetTransfersAsync));
            return result.ShowTransfersResponse.Result;
        }

        public async Task<ShowTransferByTxid> GetTransferByTxidAsync(string txid, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransferByTxidAsync(txid, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransferByTxidResponse, nameof(GetTransferByTxidAsync));
            return result.TransferByTxidResponse.Result;
        }

        public async Task<ShowTransferByTxid> GetTransferByTxidAsync(string txid, uint accountIndex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetTransferByTxidAsync(txid, accountIndex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.TransferByTxidResponse, nameof(GetTransferByTxidAsync));
            return result.TransferByTxidResponse.Result;
        }

        public async Task<Signature> SignAsync(string data, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SignAsync(data, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignResponse, nameof(SignAsync));
            return result.SignResponse.Result;
        }

        public async Task<VerifyResult> VerifyAsync(string data, string address, string signature, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.VerifyAsync(data, address, signature, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.VerifyResponse, nameof(VerifyAsync));
            return result.VerifyResponse.Result;
        }

        public async Task<ExportOutputs> ExportOutputsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ExportOutputsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportOutputsResponse, nameof(ExportOutputsAsync));
            return result.ExportOutputsResponse.Result;
        }

        public async Task<ImportOutputsResult> ImportOutputsAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ImportOutputsAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportOutputsResponse, nameof(ImportOutputsAsync));
            return result.ImportOutputsResponse.Result;
        }

        public async Task<ExportKeyImages> ExportKeyImagesAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ExportKeyImagesAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportKeyImagesResponse, nameof(ExportKeyImagesAsync));
            return result.ExportKeyImagesResponse.Result;
        }

        public async Task<ImportKeyImages> ImportKeyImagesAsync(IEnumerable<(string keyImage, string signature)> signedKeyImages, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ImportKeyImagesAsync(signedKeyImages, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportKeyImagesResponse, nameof(ImportKeyImagesAsync));
            return result.ImportKeyImagesResponse.Result;
        }

        public async Task<MakeUri> MakeUriAsync(string address, ulong amount, string recipientName, string txDescription = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.MakeUriAsync(address, amount, recipientName, txDescription, paymentId, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MakeUriResponse, nameof(MakeUriAsync));
            return result.MakeUriResponse.Result;
        }

        public async Task<ParseUri> ParseUriAsync(string uri, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ParseUriAsync(uri, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ParseUriResponse, nameof(ParseUriAsync));
            return result.ParseUriResponse.Result;
        }

        public async Task<GetAddressBook> GetAddressBookAsync(IEnumerable<uint> entries, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetAddressBookAsync(entries, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetAddressBookResponse, nameof(GetAddressBookAsync));
            return result.GetAddressBookResponse.Result;
        }

        public async Task<AddAddressBook> AddAddressBookAsync(string address, string description = null, string paymentId = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.AddAddressBookAsync(address, description, paymentId, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.AddAddressBookResponse, nameof(AddAddressBookAsync));
            return result.AddAddressBookResponse.Result;
        }

        public async Task<DeleteAddressBook> DeleteAddressBookAsync(uint index, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.DeleteAddressBookAsync(index, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.DeleteAddressBookResponse, nameof(DeleteAddressBookAsync));
            return result.DeleteAddressBookResponse.Result;
        }

        public async Task<RefreshWallet> RefreshWalletAsync(uint startHeight, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.RefreshWalletAsync(startHeight, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RefreshWalletResponse, nameof(RefreshWalletAsync));
            return result.RefreshWalletResponse.Result;
        }

        public async Task<RescanSpent> RescanSpentAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.RescanSpentAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.RescanSpentResponse, nameof(RescanSpentAsync));
            return result.RescanSpentResponse.Result;
        }

        public async Task<CreateWallet> CreateWalletAsync(string filename, string language, string password = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CreateWalletAsync(filename, language, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CreateWalletResponse, nameof(CreateWalletAsync));
            return result.CreateWalletResponse.Result;
        }

        public async Task<OpenWallet> OpenWalletAsync(string filename, string password = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.OpenWalletAsync(filename, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.OpenWalletResponse, nameof(OpenWalletAsync));
            return result.OpenWalletResponse.Result;
        }

        // Note: After you close a wallet, every subsequent query will fail.
        public async Task<CloseWallet> CloseWalletAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.CloseWalletAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.CloseWalletResponse, nameof(CloseWalletAsync));
            return result.CloseWalletResponse.Result;
        }

        public async Task<ChangeWalletPassword> ChangeWalletPasswordAsync(string oldPassword = null, string newPassword = null, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ChangeWalletPasswordAsync(oldPassword, newPassword, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ChangeWalletPasswordResponse, nameof(ChangeWalletPasswordAsync));
            return result.ChangeWalletPasswordResponse.Result;
        }

        public async Task<GetVersion> GetVersionAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.GetRpcVersionAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.GetRpcVersionResponse, nameof(GetVersionAsync));
            return result.GetRpcVersionResponse.Result;
        }

        public async Task<IsMultiSigInformation> IsMultiSigAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.IsMultiSigAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.IsMultiSigInformationResponse, nameof(IsMultiSigAsync));
            return result.IsMultiSigInformationResponse.Result;
        }

        public async Task<PrepareMultiSig> PrepareMultiSigAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.PrepareMultiSigAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.PrepareMultiSigResponse, nameof(PrepareMultiSigAsync));
            return result.PrepareMultiSigResponse.Result;
        }

        public async Task<MakeMultiSig> MakeMultiSigAsync(IEnumerable<string> multiSigInfo, uint threshold, string password, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.MakeMultiSigAsync(multiSigInfo, threshold, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.MakeMultiSigResponse, nameof(MakeMultiSigAsync));
            return result.MakeMultiSigResponse.Result;
        }

        public async Task<ExportMultiSigInformation> ExportMultiSigInfoAsync(CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ExportMultiSigInfoAsync(token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ExportMultiSigInfoResponse, nameof(ExportMultiSigInfoAsync));
            return result.ExportMultiSigInfoResponse.Result;
        }

        public async Task<ImportMultiSigInformation> ImportMultiSigInfoAsync(IEnumerable<string> info, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.ImportMultiSigInfoAsync(info, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.ImportMultiSigInfoResponse, nameof(ImportMultiSigInfoAsync));
            return result.ImportMultiSigInfoResponse.Result;
        }

        public async Task<FinalizeMultiSig> FinalizeMultiSigAsync(IEnumerable<string> multiSigInfo, string password, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.FinalizeMultiSigAsync(multiSigInfo, password, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.FinalizeMultiSigResponse, nameof(FinalizeMultiSigAsync));
            return result.FinalizeMultiSigResponse.Result;
        }

        public async Task<SignMultiSigTransactionResult> SignMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SignMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SignMultiSigTransactionResponse, nameof(SignMultiSigAsync));
            return result.SignMultiSigTransactionResponse.Result;
        }

        public async Task<SubmitMultiSig> SubmitMultiSigAsync(string txDataHex, CancellationToken token = default)
        {
            var result = await _moneroRpcCommunicator.SubmitMultiSigAsync(txDataHex, token).ConfigureAwait(false);
            ErrorGuard.ThrowIfResultIsNull(result?.SubmitMultiSigTransactionResponse, nameof(SubmitMultiSigAsync));
            return result.SubmitMultiSigTransactionResponse.Result;
        }
    }
}